namespace CodeChallenge;

public interface IInputProvider<in TPuzzle, TOutput>
{
    Task<IEnumerable<TOutput>> GetInputAsync(TPuzzle challengeSelection);
}