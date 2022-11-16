namespace CodeChallenge.AdventOfCode;

using System.Text;

using CodeChallenge;

internal class AdventOfCodeInputReader : IInputReader<AdventOfCodeChallengeSelection>
{
    public async Task<IEnumerable<string>> GetInputAsync(AdventOfCodeChallengeSelection challengeSelection)
    {
        var filepath = GetInputFilePath(challengeSelection.Year, challengeSelection.Day);
        using var streamReader = new StreamReader(filepath, Encoding.UTF8);
        return (await streamReader.ReadToEndAsync().ConfigureAwait(false))
            .Split('\n')
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Trim());
    }

    private static string GetInputFilePath(int year, int day) =>
        Path.Combine(
            Environment.CurrentDirectory,
            $"Resources/AdventOfCode/{year:0000}/Day{day:00}.txt"
        );
}