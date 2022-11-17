namespace CodeChallenge.TomsDataOnion;

using CodeChallenge;

[AttributeUsage(AttributeTargets.Class)]
public class TomsDataOnionSolutionAttribute : SolutionAttribute
{
    public int Layer { get; }

    public TomsDataOnionSolutionAttribute(int layer)
    {
        Layer = layer;
    }

    public override ChallengeSelection ToChallengeSelection()
    {
        return new TomsDataOnionChallengeSelection(Layer);
    }
}