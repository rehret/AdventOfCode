namespace AdventOfCode;

public interface IInputProcessor<out T>
{
    T[] Process(string[] lines);
}