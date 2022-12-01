namespace CodeChallenge.Core.IO.InputProviders;

internal class GroupedStringInputProvider<TChallengeSelection>
    : AbstractGroupedInputProvider<TChallengeSelection, IEnumerable<IEnumerable<string>>>
    where TChallengeSelection : ChallengeSelection
{
    public GroupedStringInputProvider(IInputReader<TChallengeSelection> inputReader)
        : base(inputReader)
    { }

    protected override IEnumerable<IEnumerable<string>> ParseGroupedInput(IEnumerable<IEnumerable<string>> input) => input;
}