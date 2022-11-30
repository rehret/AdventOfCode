namespace CodeChallenge.Runner.Modules;

using System.CommandLine;

using Autofac;

using CodeChallenge.Runner.Helpers;

using Microsoft.Extensions.Logging;

internal class CommandModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = AssemblyHelpers.GetReferencedAssemblies(false);
        builder.RegisterAssemblyTypes(assemblies)
            .AssignableTo<Command>()
            .As<Command>();

        builder.Register(ctx =>
        {
            var commands = ctx.Resolve<IEnumerable<Command>>();

            var rootCommand = new RootCommand("Code Challenge runner")
            {
                TreatUnmatchedTokensAsErrors = true
            };

            foreach (var command in commands)
            {
                rootCommand.AddCommand(command);
            }

            var logLevelOption = new Option<string>("--logLevel", "Sets the minimum log level for the console output");
            logLevelOption.FromAmong(Enum.GetNames(typeof(LogLevel)));
            rootCommand.AddGlobalOption(logLevelOption);

            return rootCommand;
        });
    }
}