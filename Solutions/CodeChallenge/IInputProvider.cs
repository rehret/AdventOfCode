namespace CodeChallenge;

public interface IInputProvider<in TChallengeSelection, TOutput>
{
    Task<IEnumerable<TOutput>> GetInputAsync(TChallengeSelection challengeSelection);
}