namespace CodeChallenge.Core.IO.InputProviderBuilder;

public interface IParsedInputBuilder<in TChallengeSelection, TOutput>
    where TChallengeSelection : ChallengeSelection
{
    IInputProvider<TChallengeSelection, TOutput> Build();
}