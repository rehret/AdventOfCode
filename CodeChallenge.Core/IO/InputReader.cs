namespace CodeChallenge.Core.IO;

using System.Text;

internal class InputReader<TChallengeSelection> : IInputReader<TChallengeSelection>
    where TChallengeSelection : ChallengeSelection
{
    private readonly IInputFilePathProvider<TChallengeSelection> _inputFilePathProvider;

    public InputReader(IInputFilePathProvider<TChallengeSelection> inputFilePathProvider)
    {
        _inputFilePathProvider = inputFilePathProvider;
    }

    public async Task<string> GetInputAsync(TChallengeSelection challengeSelection)
    {
        var filepath = GetInputFilePath(challengeSelection);
        using var streamReader = new StreamReader(filepath, Encoding.UTF8);
        return await streamReader.ReadToEndAsync().ConfigureAwait(false);
    }

    private string GetInputFilePath(TChallengeSelection challengeSelection)
    {
        return Path.Combine(
            Environment.CurrentDirectory,
            _inputFilePathProvider.GetInputFilePath(challengeSelection)
        );
    }
}