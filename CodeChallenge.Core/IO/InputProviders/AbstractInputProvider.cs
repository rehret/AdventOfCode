namespace CodeChallenge.Core.IO.InputProviders;

public abstract class AbstractInputProvider<TChallengeSelection, TOutput> : IInputProvider<TChallengeSelection, TOutput>
    where TChallengeSelection : ChallengeSelection
{
    private readonly IInputReader<TChallengeSelection> _inputReader;

    protected AbstractInputProvider(IInputReader<TChallengeSelection> inputReader)
    {
        _inputReader = inputReader;
    }

    public async Task<TOutput> GetInputAsync(TChallengeSelection challengeSelection)
    {
        var lines = (await _inputReader.GetInputAsync(challengeSelection).ConfigureAwait(false))
            .Where(line => !string.IsNullOrEmpty(line)) // Filter out empty lines
            .Select(line => line.Trim());
        return ParseLines(lines);
    }

    protected abstract TOutput ParseLines(IEnumerable<string> lines);
}