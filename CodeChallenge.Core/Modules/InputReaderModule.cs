namespace CodeChallenge.Core.Modules;

using Autofac;

using CodeChallenge.Core.IO;

internal class InputReaderModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyOpenGenericTypes(ThisAssembly)
            .As(typeof(IInputReader<>));
    }
}