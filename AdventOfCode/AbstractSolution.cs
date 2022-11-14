namespace AdventOfCode;

public abstract class AbstractSolution<T> : ISolution
{
    private readonly IInputProcessor<T> _inputProcessor;

    protected AbstractSolution(IInputProcessor<T> inputProcessor)
    {
        _inputProcessor = inputProcessor;
    }

    public Task<string> SolveAsync(IEnumerable<string> input)
    {
        var processedInput = _inputProcessor.Process(input);
        return ComputeSolutionAsync(processedInput);
    }

    protected abstract Task<string> ComputeSolutionAsync(IEnumerable<T> input);
}