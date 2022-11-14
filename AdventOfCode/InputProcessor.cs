namespace AdventOfCode;

public abstract class InputProcessor<T> : IInputProcessor<T>
{
    public T[] Process(string[] lines)
    {
        return lines.Select(ProcessLine).ToArray();
    }

    protected abstract T ProcessLine(string line);
}