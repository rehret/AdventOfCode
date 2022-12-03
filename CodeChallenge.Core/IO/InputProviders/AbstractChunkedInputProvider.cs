namespace CodeChallenge.Core.IO.InputProviders;

using System.Collections;

using CodeChallenge.Core.Extensions;

/// <summary>
/// Chunks lines of input, using empty lines as the delimiter
/// </summary>
/// <typeparam name="TChallengeSelection"></typeparam>
/// <typeparam name="TOutput"></typeparam>
public abstract class AbstractChunkedInputProvider<TChallengeSelection, TOutput> : IInputProvider<TChallengeSelection, TOutput>
    where TChallengeSelection : ChallengeSelection
    where TOutput : IEnumerable<IEnumerable>
{
    private readonly IInputReader<TChallengeSelection> _inputReader;
    private readonly Func<string, int, bool> _chunkSelector;
    private readonly ChunkWhenFlags _chunkWhenFlags;

    protected AbstractChunkedInputProvider(IInputReader<TChallengeSelection> inputReader, Func<string, int, bool>? chunkSelector = null, ChunkWhenFlags flags = ChunkWhenFlags.None)
    {
        _inputReader = inputReader;
        _chunkSelector = chunkSelector ?? ((line, _) => string.IsNullOrWhiteSpace(line));
        _chunkWhenFlags = flags;
    }

    public async Task<TOutput> GetInputAsync(TChallengeSelection challengeSelection)
    {
        // We want to use empty lines as a delimiter, so we're not filtering them from the lines returned from IInputReader<>
        var groupedInput = (await _inputReader.GetInputAsync(challengeSelection).ConfigureAwait(false))
            .Select(x => x.Trim())
            .ChunkWhen(_chunkSelector, _chunkWhenFlags);

        return ParseChunkedInput(groupedInput);
    }

    protected abstract TOutput ParseChunkedInput(IEnumerable<IEnumerable<string>> input);
}