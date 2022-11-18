namespace CodeChallenge.TomsDataOnion.Attributes;

using CodeChallenge.Core;
using CodeChallenge.Core.Attributes;

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