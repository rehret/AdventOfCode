namespace CodeChallenge.Core.IO.InputProviderBuilder;

public interface IStringInputBuilder<in TChallengeSelection>
    where TChallengeSelection : ChallengeSelection
{
    IParsedInputBuilder<TChallengeSelection, string> AsString();
    IParsedInputBuilder<TChallengeSelection, TOutput> ParseAs<TOutput>(IFormatProvider? formatProvider = null)
        where TOutput : IParsable<TOutput>;
    IParsedInputBuilder<TChallengeSelection, TOutput> ParseUsing<TOutput>(Func<string, TOutput> parser);
    IParsedInputBuilder<TChallengeSelection, TOutput> ParseUsing<TOutput>(Func<string, Task<TOutput>> parser);
}