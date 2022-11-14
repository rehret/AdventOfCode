namespace AdventOfCode.IoC;

using Autofac;

public class AdventOfCodeModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<InputReader>().As<IInputReader>();
    }
}