namespace CodeChallenge.TomsDataOnion;

internal class TomsDataOnionOutputWriter
    : ITomsDataOnionOutputWriter
{
    public async Task WriteOutput(TomsDataOnionChallengeSelection challengeSelection, string result)
    {
        var outputFileWriter = new StreamWriter(GetOutputFilePath(challengeSelection));
        await using var _ = outputFileWriter.ConfigureAwait(false);
        await outputFileWriter.WriteAsync(result).ConfigureAwait(false);
    }

    private static string GetOutputFilePath(TomsDataOnionChallengeSelection challengeSelection) =>
        Path.Combine(
            Environment.CurrentDirectory,
            $"Resources/TomsDataOnion/Layer{challengeSelection.Layer + 1:0}.txt"
        );
}