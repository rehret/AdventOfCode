namespace CodeChallenge.TomsDataOnion.Decoders;

using System.Diagnostics.Contracts;
using System.Text;

internal class Ascii85Decoder
    : IAscii85Decoder
{
    private const byte DecodeBlockSize = 5;

    private const char TrailingPaddingCharacter = 'u';
    private const char ThirtyTwoBitZeroShortcut = 'z';

    public async Task DecodeAsync(Stream inputStream, Stream outputStream)
    {
        // Create a reader and writer for the input and output streams
        // Configure the StreamWriter to leave the underlying output Stream open so it can be used later
        using var reader = new BinaryReader(inputStream, Encoding.UTF8);
        var writer = new BinaryWriter(outputStream, Encoding.UTF8, true);
        await using var _ = writer.ConfigureAwait(false);

        var state = new DecodeState(0, 0, 0);

        while (reader.PeekChar() != -1)
        {
            state = ProcessAscii85Byte(state with { CurrentAscii85Byte = (byte)reader.ReadChar() });

            // Once we have a full 32bit value, write the bytes to the stream
            if (state.NumProcessedAscii85Bytes == DecodeBlockSize)
            {
                WriteBytesToStream(writer, state.CurrentByteWord);
                state = state with
                {
                    NumProcessedAscii85Bytes = 0,
                    CurrentByteWord = 0
                };
            }
        }

        // If the last block was truncated, add the padding and write the valid bytes
        var numIgnoredBytes = (byte)(DecodeBlockSize - state.NumProcessedAscii85Bytes);
        if (numIgnoredBytes > 0)
        {
            PadLastBlockAdWriteToStream(writer, state, numIgnoredBytes);
        }

        writer.Flush();
        outputStream.Seek(0, SeekOrigin.Begin);
    }

    /// <summary>
    /// Processes the Ascii85 byte on <paramref name="state"/>, updating <see cref="DecodeState.CurrentByteWord"/> appropriately
    /// </summary>
    /// <param name="state">Current <see cref="DecodeState"/> with the next Ascii85 byte to process</param>
    /// <returns></returns>
    /// <exception cref="Exception">Thrown when the Ascii85 'z' shortcut is found in the middle of an Ascii85 block</exception>
    [Pure]
    private static DecodeState ProcessAscii85Byte(DecodeState state)
    {
        if (state.CurrentAscii85Byte == ThirtyTwoBitZeroShortcut)
        {
            if (state.NumProcessedAscii85Bytes != 0) throw new Exception("Found the Ascii85 'z' shortcut in the middle of a block of characters");
            return HandleThirtyTwoBitZeroShortcut(state);
        }

        return !char.IsWhiteSpace((char)state.CurrentAscii85Byte) ? HandleNonWhiteSpaceByte(state) : state;
    }

    /// <summary>
    /// Handles the case when the 32bit zeros shortcut ('z') is found in the input
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    [Pure]
    private static DecodeState HandleThirtyTwoBitZeroShortcut(DecodeState state)
    {
        return state with
        {
            CurrentByteWord = 0,
            NumProcessedAscii85Bytes = DecodeBlockSize
        };
    }

    /// <summary>
    /// Handles non-whitespace bytes from the input
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    [Pure]
    private static DecodeState HandleNonWhiteSpaceByte(DecodeState state)
    {
        return AddAscii85ByteToUInt(state) with
        {
            NumProcessedAscii85Bytes = (byte)(state.NumProcessedAscii85Bytes + 1)
        };
    }

    /// <summary>
    /// Decode Ascii85 byte into <see cref="DecodeState.CurrentByteWord"/>
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    [Pure]
    private static DecodeState AddAscii85ByteToUInt(DecodeState state)
    {
        const byte offset = 33;
        const byte radix = 85;

        return state with
        {
            CurrentByteWord = state.CurrentByteWord + (byte)(state.CurrentAscii85Byte - offset) * (uint)Math.Pow(radix, DecodeBlockSize - state.NumProcessedAscii85Bytes - 1)
        };
    }

    private static void PadLastBlockAdWriteToStream(
        BinaryWriter writer,
        DecodeState state,
        byte numIgnoredBytes
    )
    {
        state = state with
        {
            CurrentAscii85Byte = (byte)TrailingPaddingCharacter
        };
        for (var i = 0; i < numIgnoredBytes; i++)
        {
            state = AddAscii85ByteToUInt(state) with
            {
                NumProcessedAscii85Bytes = (byte)(state.NumProcessedAscii85Bytes + 1)
            };
        }

        WriteBytesToStream(writer, state.CurrentByteWord, numIgnoredBytes);
    }

    private static void WriteBytesToStream(BinaryWriter writer, uint currentByteWord, byte numIgnoredBytes = 0)
    {
#if BIGENDIAN
        for (var i = 0; i < sizeof(uint) - numIgnoredBytes; i++)
        {
            // Write bytes, from left to right
            writer.Write((byte)(utf8Bytes >> (i * 8)));
        }
#else
        for (var i = 0; i < sizeof(uint) - numIgnoredBytes; i++)
        {
            // Write bytes, from right to left
            writer.Write((byte)((currentByteWord << (i * 8)) >> 24));
        }
#endif
    }

    private record DecodeState(uint CurrentByteWord, byte CurrentAscii85Byte, byte NumProcessedAscii85Bytes);
}