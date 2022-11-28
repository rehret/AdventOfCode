namespace CodeChallenge.Core.Console;

using System.CommandLine;
using System.CommandLine.Binding;
using System.Diagnostics;

using Autofac.Core.Registration;

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
            ILogger? logger = null;
            try
            {
                var solution = _solutionFactory(challengeSelection);
                logger = _loggerFactory.CreateLogger(solution.GetType());

                var stopwatch = new Stopwatch();
                var result = await solution.SolveAsync(stopwatch).ConfigureAwait(false);
                Console.WriteLine(result);
                logger.LogDebug("Solution execution took {SolutionExecutionDuration}ms", stopwatch.ElapsedMilliseconds);
            }
            catch (ComponentNotRegisteredException componentNotRegisteredException) when (componentNotRegisteredException.Message.Contains(nameof(ISolution)))
            {
                (logger ?? _loggerFactory.CreateLogger(GetType())).LogError("Solution has not been registered: {SolutionErrorMessage}", componentNotRegisteredException.Message);
            }
            catch (Exception ex) when (ex is FileNotFoundException or DirectoryNotFoundException)
            {
                (logger ?? _loggerFactory.CreateLogger(GetType())).LogError("Could not find input file: '{InputFile}'",
                    ex switch
                    {
                        FileNotFoundException fileNotFoundException           => fileNotFoundException.FileName,
                        DirectoryNotFoundException directoryNotFoundException => directoryNotFoundException.Message,
                        _                                                     => ""
                    });
            }
        }, Binder);

        return command;
    }
}