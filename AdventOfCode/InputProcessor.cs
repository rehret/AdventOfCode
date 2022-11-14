namespace AdventOfCode;

public abstract class InputProcessor<T> : IInputProcessor<T>
{
    public IEnumerable<T> Process(IEnumerable<string> lines)
    {
        return lines.Select(ProcessLine);
    }

    protected abstract T ProcessLine(string line);
}