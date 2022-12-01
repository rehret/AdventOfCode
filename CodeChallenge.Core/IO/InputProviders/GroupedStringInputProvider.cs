namespace CodeChallenge.Core.IO.InputProviders;

internal class GroupedStringInputProvider<TChallengeSelection> : AbstractGroupedInputProvider<TChallengeSelection, string>
    where TChallengeSelection : ChallengeSelection
{
    public GroupedStringInputProvider(IInputFilePathProvider<TChallengeSelection> inputFilePathProvider)
        : base(inputFilePathProvider)
    { }

    protected override IEnumerable<string> ParseLineGroup(IEnumerable<string> input) => input;
}