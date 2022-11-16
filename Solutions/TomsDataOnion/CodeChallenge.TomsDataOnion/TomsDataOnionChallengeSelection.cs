namespace CodeChallenge.TomsDataOnion;

using CodeChallenge;

public record TomsDataOnionChallengeSelection(int Layer) : ChallengeSelection
{
    public static bool TryParse(string input, out ChallengeSelection challengeSelection)
    {
        if (int.TryParse(input, out var layer))
        {
            challengeSelection = new TomsDataOnionChallengeSelection(layer);
            return true;
        }

        challengeSelection = new TomsDataOnionChallengeSelection(-1);
        return false;
    }
}