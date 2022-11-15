namespace AdventOfCode;

public interface IInputReader
{
    Task<IEnumerable<string>> GetInputAsync(int year, int day);
}