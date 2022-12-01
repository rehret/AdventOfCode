namespace CodeChallenge.Core.IO.InputProviders;

/// <summary>
/// Groups lines of input, using empty lines as the delimiter
/// </summary>
/// <typeparam name="TChallengeSelection"></typeparam>
/// <typeparam name="TOutput"></typeparam>
public abstract class AbstractGroupedInputProvider<TChallengeSelection, TOutput> : IInputProvider<TChallengeSelection, TOutput>
    where TChallengeSelection : ChallengeSelection
{
    private readonly IInputReader<TChallengeSelection> _inputReader;

    protected AbstractGroupedInputProvider(IInputReader<TChallengeSelection> inputReader)
    {
        _inputReader = inputReader;
    }

    public async Task<TOutput> GetInputAsync(TChallengeSelection challengeSelection)
    {
        // We want to use empty lines as a delimiter, so we're not filtering them from the lines returned from IInputReader<>
        var groupedInput = (await _inputReader.GetInputAsync(challengeSelection).ConfigureAwait(false))
            .Select(x => x.Trim())
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
            .FinalList;

        return ParseGroupedInput(groupedInput);
    }

    protected abstract TOutput ParseGroupedInput(IEnumerable<IEnumerable<string>> input);
}