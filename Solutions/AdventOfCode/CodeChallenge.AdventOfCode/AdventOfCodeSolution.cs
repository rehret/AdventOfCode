namespace CodeChallenge.AdventOfCode;

using System.Diagnostics;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core;
using CodeChallenge.Core.IO;

internal abstract class AdventOfCodeSolution<TInput, TResult> : AbstractSolution<AdventOfCodeSolutionAttribute, AdventOfCodeChallengeSelection>
{
    private readonly IInputProvider<AdventOfCodeChallengeSelection, TInput> _inputProvider;

    protected AdventOfCodeSolution(IInputProvider<AdventOfCodeChallengeSelection, TInput> inputProvider)
    {
        _inputProvider = inputProvider;
    }

    public override async Task<string> SolveAsync(Stopwatch? stopwatch = null)
    {
        var input = await _inputProvider.GetInputAsync(GetChallengeSelection()).ConfigureAwait(false);
        stopwatch?.Start();
        var result = await ComputeSolutionAsync(input).ConfigureAwait(false);
        stopwatch?.Stop();
        return GetStringFromResult(result);
    }

    internal virtual Task<TResult> ComputeSolutionAsync(TInput input) => Task.FromResult(ComputeSolution(input));

    protected virtual TResult ComputeSolution(TInput input) => throw new NotImplementedException();

    protected virtual string GetStringFromResult(TResult result) => result?.ToString() ?? string.Empty;

    protected override AdventOfCodeChallengeSelection BuildChallengeSolutionFromAttribute(AdventOfCodeSolutionAttribute attribute)
    {
        return new AdventOfCodeChallengeSelection(attribute.Year, attribute.Day, attribute.Puzzle);
    }
}