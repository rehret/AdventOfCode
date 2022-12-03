namespace CodeChallenge.Core.IO.InputProviders;

/// <inheritdoc cref="AbstractChunkedInputProvider{TChallengeSelection,TOutput}"/>
internal class ChunkedStringInputProvider<TChallengeSelection>
    : AbstractChunkedInputProvider<TChallengeSelection, IEnumerable<IEnumerable<string>>>
    where TChallengeSelection : ChallengeSelection
{
    public ChunkedStringInputProvider(IInputReader<TChallengeSelection> inputReader)
        : base(inputReader)
    { }

    protected override IEnumerable<IEnumerable<string>> ParseChunkedInput(IEnumerable<IEnumerable<string>> input) => input;
}