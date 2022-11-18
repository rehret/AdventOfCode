namespace CodeChallenge.TomsDataOnion.IO;

internal interface ITomsDataOnionOutputWriter
{
    Task WriteOutput(TomsDataOnionChallengeSelection challengeSelection, string result);
}