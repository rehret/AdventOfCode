namespace CodeChallenge.Core.CommandLine;

using System.CommandLine;
using System.Diagnostics;

using Microsoft.Extensions.Logging;

using Console = System.Console;

public abstract class AbstractCodeChallengeCommand<T> : Command
    where T : ChallengeSelection
{
    protected AbstractCodeChallengeCommand(string name, string? description = null) : base(name, description) { }

    protected virtual async Task ExecuteSolutionAsync(T challengeSelection, SolutionFactory solutionFactory, ILoggerFactory loggerFactory)
    {
        var solution = solutionFactory(challengeSelection);
        var logger = loggerFactory.CreateLogger(solution.GetType());

        var stopwatch = new Stopwatch();
        var result = await solution.SolveAsync(stopwatch).ConfigureAwait(false);
        Console.WriteLine(result);
        logger.LogDebug("Solution execution took {SolutionExecutionDuration}ms", stopwatch.ElapsedMilliseconds);
    }
}