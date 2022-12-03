namespace CodeChallenge.Core.IO.InputProviders;

using CodeChallenge.Core.Extensions;

/// <summary>
/// Chunks lines of input, using empty lines as the delimiter
/// </summary>
/// <typeparam name="TChallengeSelection"></typeparam>
/// <typeparam name="TOutput"></typeparam>
public abstract class AbstractChunkedInputProvider<TChallengeSelection, TOutput> : IInputProvider<TChallengeSelection, TOutput>
    where TChallengeSelection : ChallengeSelection
{
    private readonly IInputReader<TChallengeSelection> _inputReader;

    protected AbstractChunkedInputProvider(IInputReader<TChallengeSelection> inputReader)
    {
        _inputReader = inputReader;
    }

    public async Task<TOutput> GetInputAsync(TChallengeSelection challengeSelection)
    {
        // We want to use empty lines as a delimiter, so we're not filtering them from the lines returned from IInputReader<>
        var groupedInput = (await _inputReader.GetInputAsync(challengeSelection).ConfigureAwait(false))
            .Select(x => x.Trim())
            .ChunkWhen(string.IsNullOrWhiteSpace);

        return ParseChunkedInput(groupedInput);
    }

    protected abstract TOutput ParseChunkedInput(IEnumerable<IEnumerable<string>> input);
}