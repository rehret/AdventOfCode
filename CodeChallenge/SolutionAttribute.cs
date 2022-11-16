namespace CodeChallenge;

[AttributeUsage(AttributeTargets.Class)]
public abstract class SolutionAttribute : Attribute
{
    public abstract ChallengeSelection ToPuzzleSelection();
}