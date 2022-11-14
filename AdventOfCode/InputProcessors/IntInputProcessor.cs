namespace AdventOfCode.InputProcessors;

internal class IntInputProcessor : InputProcessor<int>
{
    protected override int ProcessLine(string line)
    {
        return int.Parse(line);
    }
}