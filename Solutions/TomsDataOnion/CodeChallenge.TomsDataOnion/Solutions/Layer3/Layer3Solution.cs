namespace CodeChallenge.TomsDataOnion.Solutions.Layer3;

using System.Text;

[TomsDataOnionSolution(3)]
internal class Layer3Solution : TomsDataOnionSolution
{
    // All output starts with "==[ Layer X/6: ..."
    private const string ExpectedFirstCharacters = "==[ Layer 4/6: ";

    // The second set of 32 bytes are padding using "=" and ending in \n\n plus the first two characters of the layer text
    // We can't guarantee that ALL of the first 28 bytes are "=", but we can improve our chances of
    // getting it correct by using the ExpectedFirstCharacters instead of fully relying on this 32-byte segment
    private static readonly string ExpectedPaddingAt32ByteOffset = new string('=', 28) + "\n\n"
        // NOTE: these two characters were found by using spaces instead and then figuring out which
        // characters made sense based on the mostly-complete output
        + "Wh";

    private const int KeyLength = 32;

    public Layer3Solution(IInputProvider<TomsDataOnionChallengeSelection, byte> inputProvider, ITomsDataOnionOutputWriter outputWriter)
        : base(inputProvider, outputWriter)
    { }

    protected override IEnumerable<byte> Decode(IEnumerable<byte> input)
    {
        var inputArray = input.ToArray();
        var key = new byte[KeyLength];

        // compute first ExpectedFirstCharacters.Length() characters using the known output
        var expectedCharacters = Encoding.UTF8.GetBytes(ExpectedFirstCharacters);
        foreach (var (character, i) in expectedCharacters.Select((c, index) => (c, index)))
        {
            key[i] = (byte)(inputArray[i] ^ character);
        }

        // compute the remaining key bytes by using the likely padding characters
        expectedCharacters = Encoding.UTF8.GetBytes(ExpectedPaddingAt32ByteOffset);
        foreach (var (character, i) in expectedCharacters.Select((c, i) => (c, i)))
        {
            key[i] = (byte)(inputArray[i + 32] ^ character);
        }

        return inputArray
            .Select((currentByte, i) => (byte)(currentByte ^ key[i % KeyLength]))
            .ToArray();
    }
}