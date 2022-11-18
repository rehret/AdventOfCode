namespace CodeChallenge.Runner.Modules;

using Autofac;

using CodeChallenge.Core.Console;
using CodeChallenge.Core.IO;
using CodeChallenge.Runner.Helpers;

internal class ChallengeSelectionArgumentParserModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(AssemblyHelpers.GetReferencedAssemblies())
            .AssignableTo<IChallengeArgumentParser>()
            .AsImplementedInterfaces();
    }
}