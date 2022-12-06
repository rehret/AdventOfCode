namespace CodeChallenge.Core.IO.InputProviders;

internal class SingleLineStringInputProvider<TChallengeSelection> : AbstractInputProvider<TChallengeSelection, string>
    where TChallengeSelection : ChallengeSelection
{
    public SingleLineStringInputProvider(IInputReader<TChallengeSelection> inputReader) : base(inputReader) { }

    protected override string ParseLines(IEnumerable<string> lines)
    {
        return lines.Single(line => !string.IsNullOrWhiteSpace(line)).Trim();
    }
}