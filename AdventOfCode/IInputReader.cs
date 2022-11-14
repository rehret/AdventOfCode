namespace AdventOfCode;

public interface IInputReader
{
    Task<string> GetInputAsync(int year, int day);
}