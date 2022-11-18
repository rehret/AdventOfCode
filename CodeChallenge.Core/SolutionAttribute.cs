namespace CodeChallenge.Core;

[AttributeUsage(AttributeTargets.Class)]
public abstract class SolutionAttribute : Attribute
{
    public abstract ChallengeSelection ToChallengeSelection();
}