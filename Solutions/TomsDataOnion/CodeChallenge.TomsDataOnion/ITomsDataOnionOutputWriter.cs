namespace CodeChallenge.TomsDataOnion;

internal interface ITomsDataOnionOutputWriter
{
    Task WriteOutput(TomsDataOnionChallengeSelection challengeSelection, string result);
}