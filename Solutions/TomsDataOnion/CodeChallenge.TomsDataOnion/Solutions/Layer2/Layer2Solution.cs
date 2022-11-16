namespace CodeChallenge.TomsDataOnion.Solutions.Layer2;

[TomsDataOnionSolution(2)]
internal class Layer2Solution : TomsDataOnionSolution
{
    private const byte ParityBitMask = 0x01;

    public Layer2Solution(IInputProvider<TomsDataOnionChallengeSelection, byte> inputProvider, ITomsDataOnionOutputWriter outputWriter)
        : base(inputProvider, outputWriter)
    { }

    protected override IEnumerable<byte> Decode(IEnumerable<byte> input)
    {
        var acceptedBytes = GetValidBytes(input);
        return DiscardParityBits(acceptedBytes).ToArray();
    }

    /// <summary>
    /// Checks parity bits and discards bad bytes
    /// </summary>
    /// <param name="rawInput"></param>
    /// <returns></returns>
    private static IEnumerable<byte> GetValidBytes(IEnumerable<byte> rawInput)
    {
        return rawInput
            .Where(currentByte =>
            {
                byte mask = 0b10000000;
                byte bitCount = 0;
                for (byte i = 0; i < 7; i++)
                {
                    if ((currentByte & mask) > 0)
                    {
                        bitCount++;
                    }

                    mask >>= 1;
                }

                return (bitCount + (currentByte & ParityBitMask)) % 2 == 0;
            });
    }

    /// <summary>
    /// Discard parity bits, shifting next bits over to take its place
    /// </summary>
    /// <param name="rawInput"></param>
    /// <returns></returns>
    private static IEnumerable<byte> DiscardParityBits(IEnumerable<byte> rawInput)
    {
        var output = new List<byte>();
        // we're building the output one byte at a time, once we have a full byte, we add it to the output
        byte newByte = 0;
        byte newByteSize = 0;

        foreach (var currentByte in rawInput)
        {
            // process bits in byte from most-significant to least-significant
            for (byte i = 7; i > 0; i--)
            {
                // shift current bit to rightmost position and bitwise-and with 0x01 mask to isolate the bit
                var currentBit = (byte)((currentByte >> i) & ParityBitMask);

                // set the current bit on our byte
                newByte |= currentBit;
                newByteSize++;

                // if we've set all 8 bits, add the byte to the output
                if (newByteSize == 8)
                {
                    output.Add(newByte);
                    newByte = 0;
                    newByteSize = 0;
                }

                // shift our byte to prepare for the next bit
                newByte <<= 1;
            }
        }

        return output;
    }
}