namespace CodeChallenge.TomsDataOnion;

using CodeChallenge.TomsDataOnion.Configuration;

using Microsoft.Extensions.Options;

internal class TomsDataOnionOutputWriter
    : ITomsDataOnionOutputWriter
{
    private readonly TomsDataOnionConfiguration _configuration;

    public TomsDataOnionOutputWriter(IOptions<TomsDataOnionConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public async Task WriteOutput(TomsDataOnionChallengeSelection challengeSelection, string result)
    {
        var outputFileWriter = new StreamWriter(GetOutputFilePath(challengeSelection));
        await using var _ = outputFileWriter.ConfigureAwait(false);
        await outputFileWriter.WriteAsync(result).ConfigureAwait(false);
    }

    private string GetOutputFilePath(TomsDataOnionChallengeSelection challengeSelection) =>
        Path.Combine(
            Environment.CurrentDirectory,
            $"Resources/TomsDataOnion/Layer{challengeSelection.Layer + 1:0}{_configuration.OutputFileSuffix}.txt"
        );
}