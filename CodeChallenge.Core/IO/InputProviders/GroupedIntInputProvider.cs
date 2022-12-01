namespace CodeChallenge.Core.IO.InputProviders;

internal class GroupedIntInputProvider<TChallengeSelection>
    : AbstractGroupedInputProvider<TChallengeSelection, IEnumerable<IEnumerable<int>>>
    where TChallengeSelection : ChallengeSelection
{
    public GroupedIntInputProvider(IInputReader<TChallengeSelection> inputReader)
        : base(inputReader)
    { }

    protected override IEnumerable<IEnumerable<int>> ParseGroupedInput(IEnumerable<IEnumerable<string>> input) =>
        input.Select(x => x.Select(int.Parse));
}