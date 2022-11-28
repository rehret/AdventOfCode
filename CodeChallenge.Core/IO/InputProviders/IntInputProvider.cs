namespace CodeChallenge.Core.IO.InputProviders;

using CodeChallenge.Core;
using CodeChallenge.Core.IO;

internal class IntInputProvider<TChallengeSelection> : IInputProvider<TChallengeSelection, IEnumerable<int>>
    where TChallengeSelection : ChallengeSelection
{
    private readonly IInputReader<TChallengeSelection> _inputReader;

    public IntInputProvider(IInputReader<TChallengeSelection> inputReader)
    {
        _inputReader = inputReader;
    }

    public async Task<IEnumerable<int>> GetInputAsync(TChallengeSelection challengeSelection)
    {
        return (await _inputReader.GetInputAsync(challengeSelection).ConfigureAwait(false))
            .Select(ProcessLine);
    }

    private static int ProcessLine(string line)
    {
        if (!int.TryParse(line, out var parsedInt))
        {
            throw new IntInputProcessorParsingException($"Could not parse value: '{line}'");
        }

        return parsedInt;
    }
}

public class IntInputProcessorParsingException : Exception
{
    public IntInputProcessorParsingException(string message, Exception? ex = null) : base(message, ex) { }
}