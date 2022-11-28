namespace CodeChallenge.AdventOfCode.Modules;

using Autofac;

using CodeChallenge.AdventOfCode.IO;

internal class InputWriterModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AdventOfCodeInputWriter>().As<IAdventOfCodeInputWriter>();
    }
}