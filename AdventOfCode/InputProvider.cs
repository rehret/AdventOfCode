namespace AdventOfCode;

public abstract class InputProvider<T> : IInputProvider<T>
{
    private readonly IInputReader _inputReader;

    protected InputProvider(IInputReader inputReader)
    {
        _inputReader = inputReader;
    }

    public async Task<IEnumerable<T>> GetInputAsync(PuzzleSelection puzzleSelection)
    {
        return (await _inputReader.GetInputAsync(puzzleSelection).ConfigureAwait(false))
            .Select(ProcessLine);
    }

    protected abstract T ProcessLine(string line);
}