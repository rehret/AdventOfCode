namespace CodeChallenge.Core.Modules;

using System.Reflection;

using Autofac;

using CodeChallenge.Core.IO;

using Module = Autofac.Module;

public abstract class InputProviderAutoRegisteringModule : Module
{
    protected InputProviderAutoRegisteringModule(Assembly thisAssembly)
    {
        ThisAssembly = thisAssembly;
    }

    protected override void Load(ContainerBuilder builder)
    {
        // Registers open implementations (IntInputProvider<T> : IInputProvider<T, int>)
        builder.RegisterAssemblyOpenGenericTypes(ThisAssembly)
            .AssignableTo(typeof(IInputProvider<,>))
            .AsImplementedInterfaces();

        // Registers closed implementations (SomePuzzleSpecificInputProvider : IInputProvider<TPuzzle, TInput>)
        builder.RegisterAssemblyTypes(ThisAssembly)
            .AsClosedTypesOf(typeof(IInputProvider<,>))
            .AsImplementedInterfaces();
    }

    protected override Assembly ThisAssembly { get; }
}