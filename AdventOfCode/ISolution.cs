namespace AdventOfCode;

public interface ISolution
{
    Task<string> SolveAsync(IEnumerable<string> input);
}