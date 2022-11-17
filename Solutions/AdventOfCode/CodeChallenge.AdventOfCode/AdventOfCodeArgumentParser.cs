namespace CodeChallenge.AdventOfCode;

using System.Text.RegularExpressions;

/// <inheritdoc cref="AbstractChallengeArgumentParser" />
internal class AdventOfCodeArgumentParser : AbstractChallengeArgumentParser
{
    private static readonly Regex ArgumentRegex = new(@"^(?<year>\d{4})/(?<day>\d{1,2})/(?<puzzle>\d{1,2}$)", RegexOptions.Compiled);

    private static readonly string[] StaticAliases = { "AdventOfCode", "Advent" };
    private static readonly string[] StaticArgumentPartNames = { "Year", "Day", "Puzzle" };

    public override string DisplayName => "Advent of Code";

    public override string[] Aliases => StaticAliases;
    public override string[] ArgumentPartNames => StaticArgumentPartNames;

    public override bool TryParse(string remainingArguments, out ChallengeSelection challengeSelection)
    {
        challengeSelection = new AdventOfCodeChallengeSelection(0, 0, 0);

        var match = ArgumentRegex.Match(remainingArguments);

        if (!int.TryParse(match.Groups["year"].Value, out var year))
        {
            return false;
        }

        if (!int.TryParse(match.Groups["day"].Value, out var day))
        {
            return false;
        }

        if (!int.TryParse(match.Groups["puzzle"].Value, out var puzzle))
        {
            return false;
        }

        challengeSelection = new AdventOfCodeChallengeSelection(year, day, puzzle);
        return true;
    }
}