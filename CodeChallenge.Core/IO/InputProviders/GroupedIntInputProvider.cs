namespace CodeChallenge.Core.IO.InputProviders;

internal class GroupedIntInputProvider<TChallengeSelection> : AbstractGroupedInputProvider<TChallengeSelection, int>
    where TChallengeSelection : ChallengeSelection
{
    public GroupedIntInputProvider(IInputFilePathProvider<TChallengeSelection> inputFilePathProvider)
        : base(inputFilePathProvider)
    { }

    protected override IEnumerable<int> ParseLineGroup(IEnumerable<string> input) => input.Select(int.Parse);
}