namespace CodeChallenge.InputProviders;

internal class StringInputProvider<TChallengeSelection> : AbstractInputProvider<TChallengeSelection, string>
    where TChallengeSelection : ChallengeSelection
{
    public StringInputProvider(IInputReader<TChallengeSelection> inputReader) : base(inputReader) { }

    protected override string ProcessLine(string line) => line;
}