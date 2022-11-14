namespace AdventOfCode;

public interface ISolution
{
    Task<string> SolveAsync(string[] input);
}