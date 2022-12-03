namespace CodeChallenge.Core.IO.InputProviders;

/// <inheritdoc cref="AbstractChunkedInputProvider{TChallengeSelection,TOutput}"/>
internal class ChunkedIntInputProvider<TChallengeSelection>
    : AbstractChunkedInputProvider<TChallengeSelection, IEnumerable<IEnumerable<int>>>
    where TChallengeSelection : ChallengeSelection
{
    public ChunkedIntInputProvider(IInputReader<TChallengeSelection> inputReader)
        : base(inputReader)
    { }

    protected override IEnumerable<IEnumerable<int>> ParseChunkedInput(IEnumerable<IEnumerable<string>> input) =>
        input.Select(x => x.Select(int.Parse));
}