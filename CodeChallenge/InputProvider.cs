namespace CodeChallenge;

public abstract class InputProvider<TPuzzle, TOutput> : IInputProvider<TPuzzle, TOutput>
    where TPuzzle : ChallengeSelection
{
    private readonly IInputReader<TPuzzle> _inputReader;

    protected InputProvider(IInputReader<TPuzzle> inputReader)
    {
        _inputReader = inputReader;
    }

    public async Task<IEnumerable<TOutput>> GetInputAsync(TPuzzle puzzleSelection)
    {
        return (await _inputReader.GetInputAsync(puzzleSelection).ConfigureAwait(false))
            .Select(ProcessLine);
    }

    protected abstract TOutput ProcessLine(string line);
}