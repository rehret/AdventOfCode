namespace CodeChallenge.Runner.IoC;

using Autofac;

using CodeChallenge.Runner.Helpers;

using Module = Autofac.Module;

internal class CodeChallengeRunnerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CodeChallengeService>().As<ICodeChallengeService>();
        builder.RegisterType<ChallengeSelectionParser>().As<IChallengeSelectionParser>();

        var assemblies = AssemblyHelpers.GetReferencedAssemblies(false);
        builder.RegisterAssemblyModules(assemblies);
    }
}