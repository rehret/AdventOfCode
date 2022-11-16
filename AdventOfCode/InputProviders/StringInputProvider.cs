namespace AdventOfCode.InputProviders;

internal class StringInputProvider : InputProvider<string>
{
    public StringInputProvider(IInputReader inputReader) : base(inputReader) { }

    protected override string ProcessLine(string line) => line;
}