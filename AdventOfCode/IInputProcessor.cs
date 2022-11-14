namespace AdventOfCode;

public interface IInputProcessor<out T>
{
    IEnumerable<T> Process(IEnumerable<string> lines);
}