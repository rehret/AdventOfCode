namespace CodeChallenge.Core.IO.InputProviderBuilder;

internal class StringInputBuilder<TChallengeSelection>
    : IStringInputBuilder<TChallengeSelection>
    where TChallengeSelection : ChallengeSelection
{
    private readonly Func<TChallengeSelection, Task<string>> _asyncInputProvider;

    public StringInputBuilder(Func<TChallengeSelection, Task<string>> asyncInputProvider)
    {
        _asyncInputProvider = asyncInputProvider;
    }

    public IParsedInputBuilder<TChallengeSelection, string> AsString()
    {
        return new ParsedInputBuilder<TChallengeSelection, string>(_asyncInputProvider);
    }

    public IParsedInputBuilder<TChallengeSelection, TOutput> ParseAs<TOutput>(IFormatProvider? formatProvider = null)
        where TOutput : IParsable<TOutput>
    {
        return new ParsedInputBuilder<TChallengeSelection, TOutput>(async challengeSelection =>
        {
            var input = await _asyncInputProvider(challengeSelection).ConfigureAwait(false);
            return TOutput.Parse(input, formatProvider);
        });
    }

    public IParsedInputBuilder<TChallengeSelection, TOutput> ParseUsing<TOutput>(Func<string, TOutput> parser)
    {
        return new ParsedInputBuilder<TChallengeSelection, TOutput>(async challengeSelection =>
        {
            var input = await _asyncInputProvider(challengeSelection).ConfigureAwait(false);
            return parser(input);
        });
    }

    public IParsedInputBuilder<TChallengeSelection, TOutput> ParseUsing<TOutput>(Func<string, Task<TOutput>> parser)
    {
        return new ParsedInputBuilder<TChallengeSelection, TOutput>(async challengeSelection =>
        {
            var input = await _asyncInputProvider(challengeSelection).ConfigureAwait(false);
            return await parser(input).ConfigureAwait(false);
        });
    }
}