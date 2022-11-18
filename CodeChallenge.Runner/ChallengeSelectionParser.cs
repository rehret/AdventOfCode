namespace CodeChallenge.Runner;

using System.Text.RegularExpressions;

using CodeChallenge.Core;

internal class ChallengeSelectionParser
    : IChallengeSelectionParser
{
    private static readonly Regex PuzzleSelectionRegex = new(@"^(?<Type>\w+)/(?<Selection>([\w\d]+/)*[\w\d]+)", RegexOptions.Compiled);

    private readonly IEnumerable<IChallengeArgumentParser> _argumentParsers;

    public ChallengeSelectionParser(IEnumerable<IChallengeArgumentParser> argumentParsers)
    {
        _argumentParsers = argumentParsers;
    }

    public bool TryParse(IEnumerable<string> args, out ChallengeSelection challengeSelection)
    {
        var match = args.Select(arg => PuzzleSelectionRegex.Match(arg)).FirstOrDefault(match => match.Success);

        if (match == null)
        {
            challengeSelection = new ChallengeSelection();
            return false;
        }

        var selectedChallengeParser = _argumentParsers.FirstOrDefault(x => x.CanBeParsed(match.Groups["Type"].Value));

        if (selectedChallengeParser == default)
        {
            challengeSelection = new ChallengeSelection();
            return false;
        }

        return selectedChallengeParser.TryParse(match.Groups["Selection"].Value, out challengeSelection);
    }

    public IEnumerable<(string ChallengeName, string Usage, string[] Aliases)> GetAllChallengeUsages()
    {
        return _argumentParsers.Select(x => (x.DisplayName, x.GetUsage(), x.Aliases));
    }
}