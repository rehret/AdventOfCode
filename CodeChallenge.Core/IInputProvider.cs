namespace CodeChallenge.Core;

public interface IInputProvider<in TChallengeSelection, TOutput>
{
    Task<IEnumerable<TOutput>> GetInputAsync(TChallengeSelection challengeSelection);
}