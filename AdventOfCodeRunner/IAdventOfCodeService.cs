namespace AdventOfCodeRunner;

internal interface IAdventOfCodeService
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}