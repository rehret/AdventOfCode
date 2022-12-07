namespace CodeChallenge.Core.IO;

public interface IInputReader<in TChallengeSelection>
    where TChallengeSelection : ChallengeSelection
{
    Task<string> GetInputAsync(TChallengeSelection challengeSelection);
}