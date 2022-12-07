namespace CodeChallenge.Core.IO.InputProviderBuilder;

public interface ILinesInputBuilder<in TChallengeSelection>
    where TChallengeSelection : ChallengeSelection
{
    IParsedInputBuilder<TChallengeSelection, IEnumerable<string>> AsStrings();
    IParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>> ParseAs<TOutput>(IFormatProvider? formatProvider = null)
        where TOutput : IParsable<TOutput>;

    IParsedInputBuilder<TChallengeSelection, TOutput> ParseUsing<TOutput>(Func<IEnumerable<string>, TOutput> parser);
    IParsedInputBuilder<TChallengeSelection, TOutput> ParseUsing<TOutput>(Func<IEnumerable<string>, Task<TOutput>> parser);
    IParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>> ParseUsing<TOutput>(Func<string, TOutput> parser);
    IParsedInputBuilder<TChallengeSelection, IEnumerable<TOutput>> ParseUsing<TOutput>(Func<string, Task<TOutput>> parser);
}