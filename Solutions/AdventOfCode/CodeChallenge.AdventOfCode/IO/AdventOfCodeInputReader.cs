namespace CodeChallenge.AdventOfCode.IO;

using CodeChallenge.Core;
using CodeChallenge.Core.IO;

internal class AdventOfCodeInputReader : AbstractInputReader<AdventOfCodeChallengeSelection>
{
    protected override string GetInputFilePath(AdventOfCodeChallengeSelection challengeSelection) =>
        Path.Combine(
            Environment.CurrentDirectory,
            $"Resources/AdventOfCode/{challengeSelection.Year:0000}/Day{challengeSelection.Day:00}.txt"
        );
}