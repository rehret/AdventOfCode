namespace CodeChallenge;

public abstract class AbstractChallengeArgumentParser : IChallengeArgumentParser
{
    public abstract string[] Aliases { get; }
    public abstract string[] ArgumentPartNames { get; }

    public abstract string DisplayName { get; }

    public bool CanBeParsed(string challengeSelectionArgument)
    {
        return Aliases.Any(x => string.Equals(x, challengeSelectionArgument, StringComparison.OrdinalIgnoreCase));
    }

    public abstract bool TryParse(string remainingArguments, out ChallengeSelection challengeSelection);

    public string GetUsage()
    {
        return $"<{Aliases.First()}>{(ArgumentPartNames.Length > 0 ? "/" + string.Join('/', ArgumentPartNames.Select(x => $"<{x}>")) : "")}";
    }
}