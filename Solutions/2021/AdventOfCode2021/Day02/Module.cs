namespace AdventOfCode2021.Day02;

using AdventOfCode2021.Day02.InputProcessors;
using AdventOfCode2021.Day02.Models;

using Autofac;

internal class Module : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SubmarineInstructionInputProcessor>().As<IInputProcessor<SubmarineInstruction>>();
    }
}