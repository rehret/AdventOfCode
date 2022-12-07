namespace CodeChallenge.Core.IO.InputProviderBuilder;

internal class ParsedInputBuilder<TChallengeSelection, TOutput>
    : IParsedInputBuilder<TChallengeSelection, TOutput>
    where TChallengeSelection : ChallengeSelection
{
    private readonly Func<TChallengeSelection, Task<TOutput>> _asyncParsedInputProvider;

    public ParsedInputBuilder(Func<TChallengeSelection, Task<TOutput>> asyncParsedInputProvider)
    {
        _asyncParsedInputProvider = asyncParsedInputProvider;
    }

    public IInputProvider<TChallengeSelection, TOutput> Build()
    {
        return new InputProvider<TChallengeSelection, TOutput>(_asyncParsedInputProvider);
    }
}