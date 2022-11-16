namespace CodeChallenge.AdventOfCode;

using System.Reflection;

using CodeChallenge;

internal abstract class AdventOfCodeSolution<TInput, TResult> : ISolution
{
    private readonly IInputProvider<AdventOfCodeChallengeSelection, TInput> _inputProvider;

    protected AdventOfCodeSolution(IInputProvider<AdventOfCodeChallengeSelection, TInput> inputProvider)
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

    private AdventOfCodeChallengeSelection GetPuzzleSelection()
    {
        var attribute = GetType().GetCustomAttribute<AdventOfCodeSolutionAttribute>();
        if (attribute == null)
        {
            throw new Exception($"Solution '{GetType().FullName}' does not have a SolutionAttribute");
        }
        return new AdventOfCodeChallengeSelection(attribute.Year, attribute.Day, attribute.Puzzle);
    }
}