namespace CodeChallenge.Core.IO.InputProviderBuilder;

public interface IChunkedInputBuilder<in TChallengeSelection>
    where TChallengeSelection : ChallengeSelection
{
    IParsedInputBuilder<TChallengeSelection, IEnumerable<IEnumerable<string>>> AsString();
    IParsedInputBuilder<TChallengeSelection, IEnumerable<IEnumerable<TOutput>>> ParseAs<TOutput>(IFormatProvider? formatProvider = null)
        where TOutput : IParsable<TOutput>;
    IParsedInputBuilder<TChallengeSelection, IEnumerable<IEnumerable<TOutput>>> ParseUsing<TOutput>(Func<string, TOutput> parser);
    IParsedInputBuilder<TChallengeSelection, IEnumerable<IEnumerable<TOutput>>> ParseUsing<TOutput>(Func<string, Task<TOutput>> parser);
    IParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>> ParseUsing<TOutput>(Func<IEnumerable<string>, TOutput> parser);
    IParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>> ParseUsing<TOutput>(Func<IEnumerable<string>, Task<TOutput>> parser);
    IParsedInputBuilder<TChallengeSelection, TOutput> ParseUsing<TOutput>(Func<IEnumerable<IEnumerable<string>>, TOutput> parser);
    IParsedInputBuilder<TChallengeSelection, TOutput> ParseUsing<TOutput>(Func<IEnumerable<IEnumerable<string>>, Task<TOutput>> parser);
}