namespace CodeChallenge.Core.IO;

public interface IInputProvider<in TChallengeSelection, TOutput>
    where TChallengeSelection : ChallengeSelection
{
    Task<TOutput> GetInputAsync(TChallengeSelection challengeSelection);
}