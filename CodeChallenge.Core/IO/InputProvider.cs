namespace CodeChallenge.Core.IO;

internal class InputProvider<TChallengeSelection, TOutput>
    : IInputProvider<TChallengeSelection, TOutput>
    where TChallengeSelection : ChallengeSelection
{
    private readonly Func<TChallengeSelection, Task<TOutput>> _asyncInputProvider;

    public InputProvider(Func<TChallengeSelection, Task<TOutput>> asyncInputProvider)
    {
        _asyncInputProvider = asyncInputProvider;
    }

    public Task<TOutput> GetInputAsync(TChallengeSelection challengeSelection) =>
        _asyncInputProvider(challengeSelection);
}