namespace CodeChallenge;

public interface IInputReader<in T>
    where T : ChallengeSelection
{
    Task<IEnumerable<string>> GetInputAsync(T puzzleSelection);
}