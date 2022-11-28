namespace CodeChallenge.AdventOfCode.IO;

using System.Text;

using CodeChallenge.Core.IO;

internal class AdventOfCodeInputReader : IInputReader<AdventOfCodeChallengeSelection>
{
    public async Task<IEnumerable<string>> GetInputAsync(AdventOfCodeChallengeSelection challengeSelection)
    {
        var filepath = FilePathHelpers.GetInputFilePath(challengeSelection);
        using var streamReader = new StreamReader(filepath, Encoding.UTF8);
        var fileContent = await streamReader.ReadToEndAsync().ConfigureAwait(false);
        return fileContent.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }
}