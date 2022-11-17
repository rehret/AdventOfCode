namespace CodeChallenge.TomsDataOnion;

/// <inheritdoc cref="AbstractChallengeArgumentParser" />
internal class TomsDataOnionArgumentParser : AbstractChallengeArgumentParser
{
    private static readonly string[] StaticAliases = { "TomsDataOnion", "Toms", "DataOnion" };
    private static readonly string[] StaticArgumentPartNames = { "Layer" };

    public override string DisplayName => "Tom's Data Onion";

    public override string[] Aliases => StaticAliases;
    public override string[] ArgumentPartNames => StaticArgumentPartNames;

    public override bool TryParse(string remainingArguments, out ChallengeSelection challengeSelection)
    {
        if (int.TryParse(remainingArguments, out var layer))
        {
            challengeSelection = new TomsDataOnionChallengeSelection(layer);
            return true;
        }

        challengeSelection = new TomsDataOnionChallengeSelection(-1);
        return false;
    }
}