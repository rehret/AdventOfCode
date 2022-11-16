namespace CodeChallenge.Runner;

using System.Text.RegularExpressions;

using CodeChallenge;
using CodeChallenge.AdventOfCode;

public static class ChallengeSelectionParser
{
    private static readonly Regex PuzzleSelectionRegex = new Regex(@"^(?<Type>\w+)/(?<Selection>([\w\d]+/)+[\w\d]+)", RegexOptions.Compiled);

    public static bool TryParse(IEnumerable<string> args, out ChallengeSelection challengeSelection)
    {
        var match = args.Select(arg => PuzzleSelectionRegex.Match(arg)).FirstOrDefault(match => match.Success);

        if (match == null)
        {
            challengeSelection = new ChallengeSelection();
            return false;
        }

        if (!TryParsePuzzleType(match.Groups["Type"].Value, out var type))
        {
            challengeSelection = new ChallengeSelection();
            return false;
        }

        challengeSelection = new ChallengeSelection();
        return type switch
        {
            PuzzleType.AdventOfCode => AdventOfCodeChallengeSelection.TryParse(match.Groups["Selection"].Value, out challengeSelection),
            _                       => false
        };
    }

    private static bool TryParsePuzzleType(string input, out PuzzleType type)
    {
        switch (input.Trim().ToLower())
        {
            case "advent":
            case "adventofcode":
            {
                type = PuzzleType.AdventOfCode;
                return true;
            }
            default:
            {
                type = PuzzleType.AdventOfCode;
                return false;
            }
        }
     }

    private enum PuzzleType
    {
        AdventOfCode
    }
}