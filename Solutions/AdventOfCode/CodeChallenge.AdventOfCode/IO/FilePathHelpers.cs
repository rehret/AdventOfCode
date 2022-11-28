namespace CodeChallenge.AdventOfCode.IO;

internal static class FilePathHelpers
{
    public static string GetInputFilePath(AdventOfCodeChallengeSelection challengeSelection) =>
        Path.Combine(
            Environment.CurrentDirectory,
            $"Resources/AdventOfCode/{challengeSelection.Year:0000}/Day{challengeSelection.Day:00}.txt"
        );
}