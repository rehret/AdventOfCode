namespace CodeChallenge.Runner;

internal interface ICodeChallengeService
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}