namespace CodeChallenge.Template.Solution;

using CodeChallenge.Core;

internal abstract class AbstractSolutionTemplateSolution<TInput, TResult> : AbstractSolution<SolutionTemplateSolutionAttribute, SolutionTemplateChallengeSelection>
{
    private readonly IInputProvider<SolutionTemplateChallengeSelection, TInput> _inputProvider;

    protected AbstractSolutionTemplateSolution(IInputProvider<SolutionTemplateChallengeSelection, TInput> inputProvider)
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

    protected override SolutionTemplateChallengeSelection BuildChallengeSolutionFromAttribute(SolutionTemplateSolutionAttribute attribute)
    {
        return new SolutionTemplateChallengeSelection();
    }
}