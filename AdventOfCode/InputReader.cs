namespace AdventOfCode;

using System.Text;

internal class InputReader
    : IInputReader
{
    public async Task<IEnumerable<string>> GetInputAsync(PuzzleSelection puzzleSelection)
    {
        var filepath = GetInputFilePath(puzzleSelection.Year, puzzleSelection.Day);
        using var streamReader = new StreamReader(filepath, Encoding.UTF8);
        return (await streamReader.ReadToEndAsync().ConfigureAwait(false))
            .Split('\n')
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Trim());
    }

    private static string GetInputFilePath(int year, int day) =>
        Path.Combine(
            Environment.CurrentDirectory,
            $"Resources/{year:0000}/Day{day:00}.txt"
        );
}