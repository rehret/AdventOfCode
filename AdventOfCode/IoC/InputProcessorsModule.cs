namespace AdventOfCode.IoC;

using Autofac;

internal class InputProcessorsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IInputProcessor<>));
    }
}