namespace CodeChallenge.Core.IO.InputProviders;

using CodeChallenge.Core.Extensions;

/// <inheritdoc cref="AbstractChunkedInputProvider{TChallengeSelection,TOutput}"/>
internal class ChunkedStringInputProvider<TChallengeSelection>
    : AbstractChunkedInputProvider<TChallengeSelection, IEnumerable<IEnumerable<string>>>
    where TChallengeSelection : ChallengeSelection
{
    public ChunkedStringInputProvider(IInputReader<TChallengeSelection> inputReader, Func<string, int, bool>? chunkSelector = null, ChunkWhenFlags chunkWhenFlags = 0x00)
        : base(inputReader, chunkSelector, chunkWhenFlags)
    { }

    protected override IEnumerable<IEnumerable<string>> ParseChunkedInput(IEnumerable<IEnumerable<string>> input) => input;
}