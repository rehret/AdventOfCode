namespace CodeChallenge.Template.Solution;

using System.Diagnostics;

using CodeChallenge.Core;
using CodeChallenge.Core.IO;
using CodeChallenge.Template.Solution.Attributes;

internal abstract class AbstractSolutionTemplateSolution<TInput, TResult> : AbstractSolution<SolutionTemplateSolutionAttribute, SolutionTemplateChallengeSelection>
{
    private readonly IInputProvider<SolutionTemplateChallengeSelection, TInput> _inputProvider;

    protected AbstractSolutionTemplateSolution(IInputProvider<SolutionTemplateChallengeSelection, TInput> inputProvider)
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

    public abstract Task<TResult> ComputeSolutionAsync(IEnumerable<TInput> input);

    protected virtual string GetStringFromResult(TResult result) => result?.ToString() ?? string.Empty;

    protected override SolutionTemplateChallengeSelection BuildChallengeSolutionFromAttribute(SolutionTemplateSolutionAttribute attribute)
    {
        return new SolutionTemplateChallengeSelection();
    }
}