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
        logger.LogDebug("Solution execution took {SolutionExecutionDuration}", TimeSpanToString(TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds)));
    }

    private static string TimeSpanToString(TimeSpan timeSpan)
    {
        if (timeSpan.TotalSeconds <= 1)
        {
            return $@"{timeSpan:%s\.%ff}s";
        }
        if (timeSpan.TotalMinutes <= 1)
        {
            return $@"{timeSpan:%s\.%ff}s";
        }
        if (timeSpan.TotalHours <= 1)
        {
            return $@"{timeSpan:%m}m";
        }
        if (timeSpan.TotalDays <= 1)
        {
            return $@"{timeSpan:%h}h";
        }

        return $@"{timeSpan:%d}d";
    }
}