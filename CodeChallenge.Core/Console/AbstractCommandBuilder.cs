namespace CodeChallenge.Core.Console;

using System.CommandLine;
using System.CommandLine.Binding;
using System.Diagnostics;

using Microsoft.Extensions.Logging;

using Console = System.Console;

public abstract class AbstractCommandBuilder<T> : ICommandBuilder
    where T : ChallengeSelection
{
    private readonly SolutionFactory _solutionFactory;
    private readonly ILoggerFactory _loggerFactory;

    protected abstract Command Command { get; }
    protected abstract BinderBase<T> Binder { get; }

    protected AbstractCommandBuilder(SolutionFactory solutionFactory, ILoggerFactory loggerFactory)
    {
        _solutionFactory = solutionFactory;
        _loggerFactory = loggerFactory;
    }

    public Command Build()
    {
        var command = Command;
        command.SetHandler(async challengeSelection =>
        {
            var solution = _solutionFactory(challengeSelection);
            var logger = _loggerFactory.CreateLogger(solution.GetType());

            var stopwatch = new Stopwatch();
            var result = await solution.SolveAsync(stopwatch).ConfigureAwait(false);
            Console.WriteLine(result);

            logger.LogDebug("Solution execution took {SolutionExecutionDuration}ms", stopwatch.ElapsedMilliseconds);
        }, Binder);

        return command;
    }
}