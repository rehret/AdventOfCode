namespace CodeChallenge.InputProviders;

internal class StringInputProvider<T> : InputProvider<T, string>
    where T : ChallengeSelection
{
    public StringInputProvider(IInputReader<T> inputReader) : base(inputReader) { }

    protected override string ProcessLine(string line) => line;
}