namespace CodeChallenge.Template.Solution.Attributes;

using CodeChallenge.Core;
using CodeChallenge.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SolutionTemplateSolutionAttribute : SolutionAttribute
{
    public override ChallengeSelection ToChallengeSelection()
    {
        return new SolutionTemplateChallengeSelection();
    }
}