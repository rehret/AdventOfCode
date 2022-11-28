namespace CodeChallenge.Core.IO.InputProviders;

using CodeChallenge.Core.IO;

internal class StringInputProvider<TChallengeSelection> : IInputProvider<TChallengeSelection, IEnumerable<string>>
    where TChallengeSelection : ChallengeSelection
{
    private readonly IInputReader<TChallengeSelection> _inputReader;

    public StringInputProvider(IInputReader<TChallengeSelection> inputReader)
    {
        _inputReader = inputReader;
    }

    public async Task<IEnumerable<string>> GetInputAsync(TChallengeSelection challengeSelection)
    {
        return await _inputReader.GetInputAsync(challengeSelection).ConfigureAwait(false);
    }
}