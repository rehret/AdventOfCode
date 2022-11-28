namespace CodeChallenge.Core.IO;

public interface IInputProvider<in TChallengeSelection, TOutput>
{
    Task<TOutput> GetInputAsync(TChallengeSelection challengeSelection);
}