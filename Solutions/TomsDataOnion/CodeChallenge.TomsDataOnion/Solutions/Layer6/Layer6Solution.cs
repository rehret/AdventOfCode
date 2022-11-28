namespace CodeChallenge.TomsDataOnion.Solutions.Layer6;

using CodeChallenge.Core.IO;
using CodeChallenge.TomsDataOnion.Attributes;
using CodeChallenge.TomsDataOnion.IO;
using CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator;

[TomsDataOnionSolution(6)]
internal class Layer6Solution : TomsDataOnionSolution
{
    public Layer6Solution(IInputProvider<TomsDataOnionChallengeSelection, IEnumerable<byte>> inputProvider, ITomsDataOnionOutputWriter outputWriter)
        : base(inputProvider, outputWriter)
    { }

    protected override IEnumerable<byte> Decode(IEnumerable<byte> input)
    {
        var program = input.ToArray();
        var emulator = new TomtelCoreI69Emulator(program.Length);
        emulator.LoadProgram(program);
        return emulator.Execute();
    }
}