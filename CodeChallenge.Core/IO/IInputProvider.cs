namespace CodeChallenge.Core.IO;

public interface IInputProvider<in TChallengeSelection, TOutput>
{
    Task<IEnumerable<TOutput>> GetInputAsync(TChallengeSelection challengeSelection);
}