namespace AdventOfCode;

using System.Reflection;

public abstract class AbstractSolution<TInput, TResult> : ISolution
{
    private readonly IInputProvider<TInput> _inputProvider;

    protected AbstractSolution(IInputProvider<TInput> inputProvider)
    {
        _inputProvider = inputProvider;
    }

    public async Task<string> SolveAsync()
    {
        var input = await _inputProvider.GetInputAsync(GetPuzzleSelection()).ConfigureAwait(false);
        var result = await ComputeSolutionAsync(input).ConfigureAwait(false);
        return GetStringFromResult(result);
    }

    public abstract Task<TResult> ComputeSolutionAsync(IEnumerable<TInput> input);

    protected virtual string GetStringFromResult(TResult result) => result?.ToString() ?? string.Empty;

    private PuzzleSelection GetPuzzleSelection()
    {
        var attribute = GetType().GetCustomAttribute<SolutionAttribute>();
        if (attribute == null)
        {
            throw new Exception($"Solution '{GetType().FullName}' does not have a SolutionAttribute");
        }
        return new PuzzleSelection(attribute.Year, attribute.Day, attribute.Puzzle);
    }
}