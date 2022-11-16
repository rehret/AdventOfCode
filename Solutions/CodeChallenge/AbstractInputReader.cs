namespace CodeChallenge;

using System.Text;

public abstract class AbstractInputReader<T> : IInputReader<T>
    where T : ChallengeSelection
{
    public async Task<IEnumerable<string>> GetInputAsync(T challengeSelection)
    {
        var filepath = GetInputFilePath(challengeSelection);
        using var streamReader = new StreamReader(filepath, Encoding.UTF8);
        return (await streamReader.ReadToEndAsync().ConfigureAwait(false))
            .Split('\n')
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Trim());
    }

    protected abstract string GetInputFilePath(T challengeSelection);
}