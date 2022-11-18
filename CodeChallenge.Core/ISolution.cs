namespace CodeChallenge.Core;

public interface ISolution
{
    Task<string> SolveAsync();
}