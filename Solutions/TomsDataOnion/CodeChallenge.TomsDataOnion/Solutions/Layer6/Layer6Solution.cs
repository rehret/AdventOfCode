namespace CodeChallenge.TomsDataOnion.Solutions.Layer6;

[TomsDataOnionSolution(6)]
internal class Layer6Solution : TomsDataOnionSolution
{
    public Layer6Solution(IInputProvider<TomsDataOnionChallengeSelection, byte> inputProvider, ITomsDataOnionOutputWriter outputWriter)
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