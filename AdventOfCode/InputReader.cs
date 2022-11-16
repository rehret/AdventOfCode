namespace AdventOfCode;

using System.Text;

public delegate IInputReader InputReaderFactory(PuzzleSelection puzzleSelection);

internal class InputReader
    : IInputReader
{
    private readonly PuzzleSelection _puzzleSelection;

    public InputReader(PuzzleSelection puzzleSelection)
    {
        _puzzleSelection = puzzleSelection;
    }

    public async Task<IEnumerable<string>> GetInputAsync()
    {
        var filepath = GetInputFilePath(_puzzleSelection.Year, _puzzleSelection.Day);
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