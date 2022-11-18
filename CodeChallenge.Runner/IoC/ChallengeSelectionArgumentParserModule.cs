namespace CodeChallenge.Runner.IoC;

using Autofac;

using CodeChallenge.Core;
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