namespace CodeChallenge.Runner.Modules;

using Autofac;

using CodeChallenge.Runner.Helpers;

using Module = Autofac.Module;

internal class CodeChallengeRunnerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = AssemblyHelpers.GetReferencedAssemblies(false);
        builder.RegisterAssemblyModules(assemblies);
    }
}