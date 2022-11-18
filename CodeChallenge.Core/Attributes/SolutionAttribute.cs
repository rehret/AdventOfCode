namespace CodeChallenge.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public abstract class SolutionAttribute : Attribute
{
    public abstract ChallengeSelection ToChallengeSelection();
}