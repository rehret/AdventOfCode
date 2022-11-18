namespace CodeChallenge.AdventOfCode;

using CodeChallenge.Core;

internal abstract class AdventOfCodeSolution<TInput, TResult> : AbstractSolution<AdventOfCodeSolutionAttribute, AdventOfCodeChallengeSelection>
{
    private readonly IInputProvider<AdventOfCodeChallengeSelection, TInput> _inputProvider;

    protected AdventOfCodeSolution(IInputProvider<AdventOfCodeChallengeSelection, TInput> inputProvider)
    {
        _inputProvider = inputProvider;
    }

    public override async Task<string> SolveAsync()
    {
        var input = await _inputProvider.GetInputAsync(GetChallengeSelection()).ConfigureAwait(false);
        var result = await ComputeSolutionAsync(input).ConfigureAwait(false);
        return GetStringFromResult(result);
    }

    public abstract Task<TResult> ComputeSolutionAsync(IEnumerable<TInput> input);

    protected virtual string GetStringFromResult(TResult result) => result?.ToString() ?? string.Empty;

    protected override AdventOfCodeChallengeSelection BuildChallengeSolutionFromAttribute(AdventOfCodeSolutionAttribute attribute)
    {
        return new AdventOfCodeChallengeSelection(attribute.Year, attribute.Day, attribute.Puzzle);
    }
}