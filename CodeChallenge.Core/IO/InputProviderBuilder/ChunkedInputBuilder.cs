namespace CodeChallenge.Core.IO.InputProviderBuilder;

internal class ChunkedInputBuilder<TChallengeSelection>
    : IChunkedInputBuilder<TChallengeSelection>
    where TChallengeSelection : ChallengeSelection
{
    private readonly Func<TChallengeSelection, Task<IEnumerable<IEnumerable<string>>>> _asyncInputProvider;

    public ChunkedInputBuilder(Func<TChallengeSelection, Task<IEnumerable<IEnumerable<string>>>> asyncInputProvider)
    {
        _asyncInputProvider = asyncInputProvider;
    }

    public IParsedInputBuilder<TChallengeSelection, IEnumerable<IEnumerable<string>>> AsString()
    {
        return new ParsedInputBuilder<TChallengeSelection, IEnumerable<IEnumerable<string>>>(_asyncInputProvider);
    }

    public IParsedInputBuilder<TChallengeSelection, IEnumerable<IEnumerable<TOutput>>> ParseAs<TOutput>(IFormatProvider? formatProvider = null)
        where TOutput : IParsable<TOutput>
    {
        return new ParsedInputBuilder<TChallengeSelection, IEnumerable<IEnumerable<TOutput>>>(async challengeSelection =>
        {
            var chunks = await _asyncInputProvider(challengeSelection).ConfigureAwait(false);
            return chunks.Select(chunk => chunk.Select(line => TOutput.Parse(line, formatProvider)));
        });
    }

    public IParsedInputBuilder<TChallengeSelection, IEnumerable<IEnumerable<TOutput>>> ParseUsing<TOutput>(Func<string, TOutput> parser)
    {
        return new ParsedInputBuilder<TChallengeSelection, IEnumerable<IEnumerable<TOutput>>>(async challengeSelection =>
        {
            return (await _asyncInputProvider(challengeSelection).ConfigureAwait(false))
                .Select(chunk => chunk.Select(parser));
        });
    }

    public IParsedInputBuilder<TChallengeSelection, IEnumerable<IEnumerable<TOutput>>> ParseUsing<TOutput>(Func<string, Task<TOutput>> parser)
    {
        return new ParsedInputBuilder<TChallengeSelection, IEnumerable<IEnumerable<TOutput>>>(async challengeSelection =>
        {
            return await Task.WhenAll((await _asyncInputProvider(challengeSelection).ConfigureAwait(false))
                    .Select(async chunk => await Task.WhenAll(chunk.Select(parser)).ConfigureAwait(false)))
                .ConfigureAwait(false);
        });
    }

    public IParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>> ParseUsing<TOutput>(Func<IEnumerable<string>, TOutput> parser)
    {
        return new ParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>>(async challengeSelection =>
            (await _asyncInputProvider(challengeSelection).ConfigureAwait(false))
            .Select(parser));
    }

    public IParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>> ParseUsing<TOutput>(Func<IEnumerable<string>, Task<TOutput>> parser)
    {
        return new ParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>>(async challengeSelection =>
            await Task.WhenAll((await _asyncInputProvider(challengeSelection).ConfigureAwait(false))
                    .Select(parser))
                .ConfigureAwait(false));
    }

    public IParsedInputBuilder<TChallengeSelection, TOutput> ParseUsing<TOutput>(Func<IEnumerable<IEnumerable<string>>, TOutput> parser)
    {
        return new ParsedInputBuilder<TChallengeSelection, TOutput>(async challengeSelection =>
        {
            var chunks = await _asyncInputProvider(challengeSelection).ConfigureAwait(false);
            return parser(chunks);
        });
    }

    public IParsedInputBuilder<TChallengeSelection, TOutput> ParseUsing<TOutput>(Func<IEnumerable<IEnumerable<string>>, Task<TOutput>> parser)
    {
        return new ParsedInputBuilder<TChallengeSelection, TOutput>(async challengeSelection =>
        {
            var chunks = await _asyncInputProvider(challengeSelection).ConfigureAwait(false);
            return await parser(chunks).ConfigureAwait(false);
        });
    }
}