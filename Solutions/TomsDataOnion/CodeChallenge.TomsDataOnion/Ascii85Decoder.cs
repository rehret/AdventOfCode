namespace CodeChallenge.TomsDataOnion;

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

        // Number of bytes we've pulled from the input stream. We grab <DecodeBlockSize> bytes before processing them.
        byte byteCount = 0;

        // 32bit value containing all the bits we've pulled from the input stream
        uint currentByteWord = 0;

        while (reader.PeekChar() != -1)
        {
            byteCount = ProcessCharacter(ref currentByteWord, byteCount, reader.ReadChar());

            // Once we have a full 32bit value, write the bytes to the stream
            if (byteCount == DecodeBlockSize)
            {
                WriteBytesToStream(writer, currentByteWord);
                byteCount = 0;
                currentByteWord = 0;
            }
        }

        // If the last block was truncated, add the padding and write the valid bytes
        var numIgnoredBytes = (byte)(DecodeBlockSize - byteCount);
        if (numIgnoredBytes > 0)
        {
            PadLastBlockAdWriteToStream(writer, currentByteWord, numIgnoredBytes, byteCount);
        }

        writer.Flush();
        outputStream.Seek(0, SeekOrigin.Begin);
    }

    private static byte ProcessCharacter(ref uint currentByteWord, byte byteCount, char @char)
    {
        if (@char == ThirtyTwoBitZeroShortcut)
        {
            return HandleThirtyTwoBitZeroShortcut(ref currentByteWord, byteCount);
        }

        return !char.IsWhiteSpace(@char) ? HandleNonWhiteSpaceByte(ref currentByteWord, byteCount, @char) : byteCount;
    }

    private static byte HandleThirtyTwoBitZeroShortcut(ref uint currentByteWord, byte byteCount)
    {
        if (byteCount != 0)
        {
            throw new Exception("Found the Ascii85 'z' shortcut in the middle of a block of characters");
        }
        currentByteWord = 0;
        return DecodeBlockSize;
    }

    private static byte HandleNonWhiteSpaceByte(ref uint currentByteWord, byte byteCount, char @char)
    {
        currentByteWord = AddAscii85ByteToUInt(currentByteWord, (byte)@char, byteCount);
        return ++byteCount;
    }

    private static uint AddAscii85ByteToUInt(uint current, byte ascii85Byte, byte currentByteOffset)
    {
        const byte offset = 33;
        const byte radix = 85;
        return current + (byte)(ascii85Byte - offset) * (uint)Math.Pow(radix, DecodeBlockSize - currentByteOffset - 1);
    }

    private static void PadLastBlockAdWriteToStream(
        BinaryWriter writer,
        uint currentByteWord,
        byte numIgnoredBytes,
        byte byteCount
    )
    {
        for (var i = 0; i < numIgnoredBytes; i++)
        {
            currentByteWord = AddAscii85ByteToUInt(currentByteWord, (byte)TrailingPaddingCharacter, (byte)(byteCount + i));
        }

        WriteBytesToStream(writer, currentByteWord, numIgnoredBytes);
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
}