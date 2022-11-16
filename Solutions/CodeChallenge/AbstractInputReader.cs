namespace CodeChallenge;

using System.Text;

public abstract class AbstractInputReader<TChallengeSelection> : IInputReader<TChallengeSelection>
    where TChallengeSelection : ChallengeSelection
{
    public async Task<IEnumerable<string>> GetInputAsync(TChallengeSelection challengeSelection)
    {
        var filepath = GetInputFilePath(challengeSelection);
        using var streamReader = new StreamReader(filepath, Encoding.UTF8);
        return (await streamReader.ReadToEndAsync().ConfigureAwait(false))
            .Split('\n')
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Trim());
    }

    protected abstract string GetInputFilePath(TChallengeSelection challengeSelection);
}