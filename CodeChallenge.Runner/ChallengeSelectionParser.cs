namespace CodeChallenge.Runner;

using System.Text.RegularExpressions;

using CodeChallenge;
using CodeChallenge.AdventOfCode;
using CodeChallenge.TomsDataOnion;

public static class ChallengeSelectionParser
{
    private static readonly Regex PuzzleSelectionRegex = new Regex(@"^(?<Type>\w+)/(?<Selection>([\w\d]+/)*[\w\d]+)", RegexOptions.Compiled);

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
            ChallengeType.AdventOfCode => AdventOfCodeChallengeSelection.TryParse(match.Groups["Selection"].Value, out challengeSelection),
            ChallengeType.TomsDataOnion => TomsDataOnionChallengeSelection.TryParse(match.Groups["Selection"].Value, out challengeSelection),
            _                       => false
        };
    }

    private static bool TryParsePuzzleType(string input, out ChallengeType type)
    {
        switch (input.Trim().ToLower())
        {
            case "advent":
            case "adventofcode":
            {
                type = ChallengeType.AdventOfCode;
                return true;
            }
            case "tomsdataonion":
            case "toms":
            case "dataonion":
            {
                type = ChallengeType.TomsDataOnion;
                return true;
            }
            default:
            {
                type = ChallengeType.AdventOfCode;
                return false;
            }
        }
     }

    private enum ChallengeType
    {
        AdventOfCode,
        TomsDataOnion
    }
}