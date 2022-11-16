namespace AdventOfCode;

public interface IInputReader
{
    Task<IEnumerable<string>> GetInputAsync();
}