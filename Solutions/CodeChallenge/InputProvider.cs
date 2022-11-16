namespace CodeChallenge;

public abstract class InputProvider<TChallengeSelection, TOutput> : IInputProvider<TChallengeSelection, TOutput>
    where TChallengeSelection : ChallengeSelection
{
    private readonly IInputReader<TChallengeSelection> _inputReader;

    protected InputProvider(IInputReader<TChallengeSelection> inputReader)
    {
        _inputReader = inputReader;
    }

    public async Task<IEnumerable<TOutput>> GetInputAsync(TChallengeSelection puzzleSelection)
    {
        return (await _inputReader.GetInputAsync(puzzleSelection).ConfigureAwait(false))
            .Select(ProcessLine);
    }

    protected abstract TOutput ProcessLine(string line);
}