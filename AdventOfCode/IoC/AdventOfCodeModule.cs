namespace AdventOfCode.IoC;

using Autofac;

internal class AdventOfCodeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<InputReader>().As<IInputReader>();
    }
}