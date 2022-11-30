namespace CodeChallenge.Core.Modules;

using System.Reflection;

using Autofac;

using CodeChallenge.Core.IO;

using Module = Autofac.Module;

public abstract class InputFilePathProviderAutoRegisteringModule : Module
{
    protected InputFilePathProviderAutoRegisteringModule(Assembly thisAssembly)
    {
        ThisAssembly = thisAssembly;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IInputFilePathProvider<>));
    }

    protected override Assembly ThisAssembly { get; }
}