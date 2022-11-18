namespace CodeChallenge.Core;

public interface IInputReader<in TChallengeSelection>
    where TChallengeSelection : ChallengeSelection
{
    Task<IEnumerable<string>> GetInputAsync(TChallengeSelection challengeSelection);
}