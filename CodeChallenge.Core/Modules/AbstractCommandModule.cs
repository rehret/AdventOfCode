namespace CodeChallenge.Core.Modules;

using System.CommandLine;
using System.CommandLine.Binding;
using System.Diagnostics;

using Autofac;

using Microsoft.Extensions.Logging;

using Console = System.Console;

public abstract class AbstractCommandModule<T> : Module
    where T : ChallengeSelection
{
    protected abstract Command Command { get; }

    protected abstract BinderBase<T> Binder { get; }

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(ctx =>
        {
            var lifetimeScope = ctx.Resolve<ILifetimeScope>();
            var command = Command;
            command.SetHandler(async (challengeSelection) =>
            {
                var solution = lifetimeScope.ResolveKeyed<ISolution>(challengeSelection);
                var logger = lifetimeScope.Resolve<ILoggerFactory>().CreateLogger(solution.GetType());
                try
                {
                    var stopwatch = Stopwatch.StartNew();
                    var result = await solution.SolveAsync().ConfigureAwait(false);
                    stopwatch.Stop();
                    Console.WriteLine(result);

                    logger.LogDebug("Solution execution took {SolutionExecutionDuration}ms", stopwatch.ElapsedMilliseconds);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "");
                }
            }, Binder);

            return command;
        });
    }
}