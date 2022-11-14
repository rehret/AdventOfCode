namespace AdventOfCode2022.Day01;

using AdventOfCode;

using Autofac;

public class Module : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Solution01>().Keyed<ISolution>((2022, 01, 01));
        builder.RegisterType<Solution02>().Keyed<ISolution>((2022, 01, 02));
    }
}