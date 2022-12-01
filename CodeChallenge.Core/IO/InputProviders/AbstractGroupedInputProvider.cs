namespace CodeChallenge.Core.IO.InputProviders;

using System.Text;

/// <summary>
/// Groups lines of input, using empty lines as the delimiter
/// </summary>
/// <typeparam name="TChallengeSelection"></typeparam>
/// <typeparam name="TOutput"></typeparam>
internal abstract class AbstractGroupedInputProvider<TChallengeSelection, TOutput> : IGroupedInputProvider<TChallengeSelection, TOutput>
    where TChallengeSelection : ChallengeSelection
{
    private readonly IInputFilePathProvider<TChallengeSelection> _inputFilePathProvider;

    protected AbstractGroupedInputProvider(IInputFilePathProvider<TChallengeSelection> inputFilePathProvider)
    {
        _inputFilePathProvider = inputFilePathProvider;
    }

    public async Task<IEnumerable<IEnumerable<TOutput>>> GetInputAsync(TChallengeSelection challengeSelection)
    {
        var filepath = GetInputFilePath(challengeSelection);
        using var streamReader = new StreamReader(filepath, Encoding.UTF8);
        return (await streamReader.ReadToEndAsync().ConfigureAwait(false))
            .Split('\n', StringSplitOptions.TrimEntries)
            .Aggregate((FinalList: new List<IEnumerable<string>>(), CurrentList: new List<string>()),
                (accumulator, currentString) =>
                {
                    if (string.IsNullOrEmpty(currentString))
                    {
                        if (!accumulator.CurrentList.Any()) return accumulator;
                        accumulator.FinalList.Add(accumulator.CurrentList);
                        return (accumulator.FinalList, new List<string>());
                    }

                    accumulator.CurrentList.Add(currentString);
                    return accumulator;
                })
            .FinalList
            .Select(ParseLineGroup);
    }

    protected abstract IEnumerable<TOutput> ParseLineGroup(IEnumerable<string> input);

    private string GetInputFilePath(TChallengeSelection challengeSelection)
    {
        return Path.Combine(
            Environment.CurrentDirectory,
            _inputFilePathProvider.GetInputFilePath(challengeSelection)
        );
    }
}