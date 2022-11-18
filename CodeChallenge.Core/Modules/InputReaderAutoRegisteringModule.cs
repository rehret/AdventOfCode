namespace CodeChallenge.Core.Modules;

using System.Reflection;

using Autofac;

using CodeChallenge.Core.IO;

using Module = Autofac.Module;

public abstract class InputReaderAutoRegisteringModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(GetAssembly())
            .AsClosedTypesOf(typeof(IInputReader<>));
    }

    protected abstract Assembly GetAssembly();
}