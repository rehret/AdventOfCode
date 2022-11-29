namespace CodeChallenge.Runner.Modules;

using System.CommandLine;

using Autofac;

using CodeChallenge.Core.Console;
using CodeChallenge.Runner.Helpers;

internal class CommandModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = AssemblyHelpers.GetReferencedAssemblies(false);
        builder.RegisterAssemblyTypes(assemblies)
            .AssignableTo<ICommandBuilder>()
            .As<ICommandBuilder>();

        builder.Register(ctx =>
        {
            var commandBuilders = ctx.Resolve<IEnumerable<ICommandBuilder>>();
            var rootCommand = new RootCommand("Code Challenge runner")
            {
                TreatUnmatchedTokensAsErrors = true
            };
            foreach (var commandBuilder in commandBuilders)
            {
                rootCommand.AddCommand(commandBuilder.Build());
            }

            var logLevelOption = new Option<string>("--logLevel", "Sets the minimum log level for the console output");
            logLevelOption.FromAmong("Trace", "Debug", "Information", "Warning", "Error", "Critical", "None");
            rootCommand.AddGlobalOption(logLevelOption);

            return rootCommand;
        });
    }
}