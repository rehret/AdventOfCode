namespace CodeChallenge.Core.Modules;

using Autofac;

using CodeChallenge.Core.IO;
using CodeChallenge.Core.IO.InputProviderBuilder;

public class InputProviderBuilderModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyOpenGenericTypes(ThisAssembly)
            .AssignableTo(typeof(IInputProviderBuilder<>))
            .As(typeof(IInputProviderBuilder<>));
    }
}