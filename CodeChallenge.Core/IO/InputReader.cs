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

    public async Task<IEnumerable<string>> GetInputAsync(TChallengeSelection challengeSelection)
    {
        var filepath = GetInputFilePath(challengeSelection);
        using var streamReader = new StreamReader(filepath, Encoding.UTF8);
        return (await streamReader.ReadToEndAsync().ConfigureAwait(false))
            .Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }

    private string GetInputFilePath(TChallengeSelection challengeSelection)
    {
        return Path.Combine(
            Environment.CurrentDirectory,
            _inputFilePathProvider.GetInputFilePath(challengeSelection)
        );
    }
}