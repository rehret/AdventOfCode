namespace CodeChallenge.InputProviders;

using CodeChallenge;

internal class IntInputProvider<TChallengeSelection> : InputProvider<TChallengeSelection, int>
    where TChallengeSelection : ChallengeSelection
{
    public IntInputProvider(IInputReader<TChallengeSelection> inputReader) : base(inputReader) { }

    protected override int ProcessLine(string line)
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