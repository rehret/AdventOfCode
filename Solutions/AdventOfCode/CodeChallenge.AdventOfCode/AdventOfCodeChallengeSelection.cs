namespace CodeChallenge.AdventOfCode;

using System.Text.RegularExpressions;

using CodeChallenge;

public record AdventOfCodeChallengeSelection(int Year, int Day, int Puzzle) : ChallengeSelection
{
    private static readonly Regex ArgumentRegex = new(@"^(?<year>\d{4})/(?<day>\d{1,2})/(?<puzzle>\d{1,2}$)", RegexOptions.Compiled);

    public static bool TryParse(string input, out ChallengeSelection challengeSelection)
    {
        challengeSelection = new AdventOfCodeChallengeSelection(0, 0, 0);

        var match = ArgumentRegex.Match(input);

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

    public override string ToString()
    {
        return $"AdventOfCode/{Year:0000}/{Day:00}/{Puzzle:00}";
    }
}