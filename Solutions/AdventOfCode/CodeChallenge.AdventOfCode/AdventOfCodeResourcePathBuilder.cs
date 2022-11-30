namespace CodeChallenge.AdventOfCode;

internal static class AdventOfCodeResourcePathBuilder
{
    public static string GetInputFilePath(AdventOfCodeChallengeSelection challengeSelection) =>
        $"Resources/AdventOfCode/{challengeSelection.Year:0000}/Day{challengeSelection.Day:00}.txt";

    public static Uri GetRemoteFilePath(AdventOfCodeChallengeSelection challengeSelection) =>
        new($"https://adventofcode.com/{challengeSelection.Year:0000}/day/{challengeSelection.Day:0}/input");

    public static Uri GetWebPageUri(AdventOfCodeChallengeSelection challengeSelection) =>
        new($"https://adventofcode.com/{challengeSelection.Year:0000}/day/{challengeSelection.Day:0}");
}