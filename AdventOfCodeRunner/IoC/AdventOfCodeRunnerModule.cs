namespace AdventOfCodeRunner.IoC;

using AdventOfCodeRunner.Helpers;

using Autofac;

using Module = Autofac.Module;

internal class AdventOfCodeRunnerModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = AssemblyHelpers.GetReferencedAssemblies(false);
        builder.RegisterAssemblyModules(assemblies);
    }
}