namespace AdventOfCodeRunner.IoC;

using AdventOfCode;

using AdventOfCodeRunner.Helpers;

using Autofac;

public class InputProvidersModules : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(AssemblyHelpers.GetReferencedAssemblies())
            .AsClosedTypesOf(typeof(IInputProvider<>));
    }
}