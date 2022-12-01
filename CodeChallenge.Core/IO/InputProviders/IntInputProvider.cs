namespace CodeChallenge.Core.IO.InputProviders;

using CodeChallenge.Core;
using CodeChallenge.Core.IO;

internal class IntInputProvider<TChallengeSelection> : AbstractInputProvider<TChallengeSelection, IEnumerable<int>>
    where TChallengeSelection : ChallengeSelection
{
    public IntInputProvider(IInputReader<TChallengeSelection> inputReader)
        : base(inputReader)
    { }

    protected override IEnumerable<int> ParseLines(IEnumerable<string> lines)
    {
        return lines.Select(ProcessLine);
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