namespace CodeChallenge.Runner;

using CodeChallenge.Core;

internal interface IChallengeSelectionParser
{
    bool TryParse(IEnumerable<string> args, out ChallengeSelection challengeSelection);
    IEnumerable<(string ChallengeName, string Usage, string[] Aliases)> GetAllChallengeUsages();
}