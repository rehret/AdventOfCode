namespace CodeChallenge.Core.IO.InputProviderBuilder;

internal class LinesInputBuilder<TChallengeSelection>
    : ILinesInputBuilder<TChallengeSelection>
    where TChallengeSelection : ChallengeSelection
{
    private readonly Func<TChallengeSelection, Task<IEnumerable<string>>> _asyncInputProvider;

    public LinesInputBuilder(Func<TChallengeSelection, Task<IEnumerable<string>>> asyncInputProvider)
    {
        _asyncInputProvider = asyncInputProvider;
    }

    public IParsedInputBuilder<TChallengeSelection, IEnumerable<string>> AsStrings()
    {
        return new ParsedInputBuilder<TChallengeSelection, IEnumerable<string>>(_asyncInputProvider);
    }

    public IParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>> ParseAs<TOutput>(IFormatProvider? formatProvider = null)
        where TOutput : IParsable<TOutput>
    {
        return new ParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>>(async challengeSelection =>
        {
            var lines = await _asyncInputProvider(challengeSelection).ConfigureAwait(false);
            return lines.Select(line => TOutput.Parse(line, formatProvider));
        });
    }

    public IParsedInputBuilder<TChallengeSelection, TOutput> ParseUsing<TOutput>(Func<IEnumerable<string>, TOutput> parser)
    {
        return new ParsedInputBuilder<TChallengeSelection, TOutput>(async challengeSelection =>
        {
            var lines = await _asyncInputProvider(challengeSelection).ConfigureAwait(false);
            return parser(lines);
        });
    }

    public IParsedInputBuilder<TChallengeSelection, TOutput> ParseUsing<TOutput>(Func<IEnumerable<string>, Task<TOutput>> parser)
    {
        return new ParsedInputBuilder<TChallengeSelection, TOutput>(async challengeSelection =>
        {
            var lines = await _asyncInputProvider(challengeSelection).ConfigureAwait(false);
            return await parser(lines).ConfigureAwait(false);
        });
    }

    public IParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>> ParseUsing<TOutput>(Func<string, TOutput> parser)
    {
        return new ParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>>(async challengeSelection =>
        {
            var lines = await _asyncInputProvider(challengeSelection).ConfigureAwait(false);
            return lines.Select(parser);
        });
    }

    public IParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>> ParseUsing<TOutput>(Func<string, Task<TOutput>> parser)
    {
        return new ParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>>(async challengeSelection =>
        {
            var lines = await _asyncInputProvider(challengeSelection).ConfigureAwait(false);
            return await Task.WhenAll(lines.Select(parser)).ConfigureAwait(false);
        });
    }
}