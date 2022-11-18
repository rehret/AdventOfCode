namespace CodeChallenge.Template.Solution;

using CodeChallenge.Core;

[AttributeUsage(AttributeTargets.Class)]
public class SolutionTemplateSolutionAttribute : SolutionAttribute
{
    public override ChallengeSelection ToChallengeSelection()
    {
        return new SolutionTemplateChallengeSelection();
    }
}