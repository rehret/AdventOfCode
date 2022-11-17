namespace CodeChallenge.TomsDataOnion;

using CodeChallenge;

public record TomsDataOnionChallengeSelection(int Layer) : ChallengeSelection
{
    public override string ToString()
    {
        return $"TomsDataOnion/{Layer:0}";
    }
}