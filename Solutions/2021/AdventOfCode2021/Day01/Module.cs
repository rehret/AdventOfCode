namespace AdventOfCode2021.Day01;

using AdventOfCode;

using Autofac;

internal class Module : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Solution01>().Keyed<ISolution>((2021, 01, 01));
        builder.RegisterType<Solution02>().Keyed<ISolution>((2021, 01, 02));
    }
}