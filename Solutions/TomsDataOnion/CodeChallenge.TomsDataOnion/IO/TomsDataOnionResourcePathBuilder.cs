namespace CodeChallenge.TomsDataOnion.IO;

internal static class TomsDataOnionResourcePathBuilder
{
    public static string GetInputFilePath(TomsDataOnionChallengeSelection challengeSelection)
    {
        return $"Resources/TomsDataOnion/Layer{challengeSelection.Layer:0}.txt";
    }

    public static string GetOutputFilePath(TomsDataOnionChallengeSelection challengeSelection, string fileSuffix)
    {
        return $"Resources/TomsDataOnion/Layer{challengeSelection.Layer + 1:0}{fileSuffix}.txt";
    }
}