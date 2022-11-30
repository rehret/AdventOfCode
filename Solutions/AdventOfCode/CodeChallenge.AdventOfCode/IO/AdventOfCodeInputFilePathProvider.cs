namespace CodeChallenge.AdventOfCode.IO;

using CodeChallenge.Core.IO;

internal class AdventOfCodeInputFilePathProvider : IInputFilePathProvider<AdventOfCodeChallengeSelection>
{
    public string GetInputFilePath(AdventOfCodeChallengeSelection challengeSelection)
    {
        return AdventOfCodeResourcePathBuilder.GetInputFilePath(challengeSelection);
    }
}