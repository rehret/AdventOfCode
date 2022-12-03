namespace CodeChallenge.Core.IO.InputProviders;

using CodeChallenge.Core.Extensions;

/// <inheritdoc cref="AbstractChunkedInputProvider{TChallengeSelection,TOutput}"/>
internal class ChunkedIntInputProvider<TChallengeSelection>
    : AbstractChunkedInputProvider<TChallengeSelection, IEnumerable<IEnumerable<int>>>
    where TChallengeSelection : ChallengeSelection
{
    public ChunkedIntInputProvider(IInputReader<TChallengeSelection> inputReader, Func<string, int, bool>? chunkSelector = null, ChunkWhenFlags chunkWhenFlags = 0x00)
        : base(inputReader, chunkSelector, chunkWhenFlags)
    { }

    protected override IEnumerable<IEnumerable<int>> ParseChunkedInput(IEnumerable<IEnumerable<string>> input) =>
        input.Select(x => x.Select(int.Parse));
}