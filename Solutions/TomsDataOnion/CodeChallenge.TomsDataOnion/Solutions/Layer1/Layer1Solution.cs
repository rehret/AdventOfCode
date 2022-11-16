namespace CodeChallenge.TomsDataOnion.Solutions.Layer1;

[TomsDataOnionSolution(1)]
internal class Layer1Solution : TomsDataOnionSolution
{
    private const int FlipBitsMask = 0b01010101;
    private const int RightmostBitMask = 0b00000001;

    public Layer1Solution(IInputProvider<TomsDataOnionChallengeSelection, byte> inputProvider, ITomsDataOnionOutputWriter outputWriter)
        : base(inputProvider, outputWriter)
    { }

    protected override IEnumerable<byte> Decode(IEnumerable<byte> input)
    {
        return input
            .Select(x => (int) x)
            .Select(currentByte =>
            {
                // flip every second bit
                currentByte ^= FlipBitsMask;

                // grab the rightmost bit, which will be discarded in the right shift
                var rightmostBit = currentByte & RightmostBitMask;

                currentByte >>= 1;

                // shift the captured rightmost bit to the leftmost bit
                var newLeftmostBit = rightmostBit << 7;
                // set the leftmost bit
                currentByte |= newLeftmostBit;

                // convert back to bytes
                return (byte)currentByte;
            });
    }
}