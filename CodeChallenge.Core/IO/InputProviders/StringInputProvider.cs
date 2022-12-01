namespace CodeChallenge.Core.IO.InputProviders;

using CodeChallenge.Core.IO;

internal class StringInputProvider<TChallengeSelection> : AbstractInputProvider<TChallengeSelection, IEnumerable<string>>
    where TChallengeSelection : ChallengeSelection
{
    public StringInputProvider(IInputReader<TChallengeSelection> inputReader)
        : base(inputReader)
    { }

    protected override IEnumerable<string> ParseLines(IEnumerable<string> lines) => lines;
}