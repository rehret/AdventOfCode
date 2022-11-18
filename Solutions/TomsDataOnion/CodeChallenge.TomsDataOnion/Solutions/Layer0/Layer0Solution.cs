namespace CodeChallenge.TomsDataOnion.Solutions.Layer0;

using CodeChallenge.Core.IO;
using CodeChallenge.TomsDataOnion.Attributes;
using CodeChallenge.TomsDataOnion.IO;

[TomsDataOnionSolution(0)]
internal class Layer0Solution : TomsDataOnionSolution
{
    public Layer0Solution(IInputProvider<TomsDataOnionChallengeSelection, byte> inputProvider, ITomsDataOnionOutputWriter outputWriter)
        : base(inputProvider, outputWriter)
    { }

    protected override IEnumerable<byte> Decode(IEnumerable<byte> input)
    {
        return input;
    }
}