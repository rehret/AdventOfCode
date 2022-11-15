namespace AdventOfCode2021.Day02;

using AdventOfCode2021.Day02.InputProcessors;
using AdventOfCode2021.Day02.Models;

using Autofac;

internal class Module : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SubmarineInstructionInputProcessor>().As<IInputProcessor<SubmarineInstruction>>();

        builder.RegisterType<Solution01>().Keyed<ISolution>((2021, 02, 01));
        builder.RegisterType<Solution02>().Keyed<ISolution>((2021, 02, 02));
    }
}