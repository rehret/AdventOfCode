namespace CodeChallenge.Core.IoC;

using System.Reflection;

using Autofac;

using Module = Autofac.Module;

public abstract class InputProviderAutoRegisteringModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        // Registers open implementations (IntInputProvider<T> : IInputProvider<T, int>)
        builder.RegisterAssemblyOpenGenericTypes(GetAssembly())
            .AssignableTo(typeof(IInputProvider<,>))
            .AsImplementedInterfaces();

        // Registers closed implementations (SomePuzzleSpecificInputProvider : IInputProvider<TPuzzle, TInput>)
        builder.RegisterAssemblyTypes(GetAssembly())
            .AsClosedTypesOf(typeof(IInputProvider<,>));
    }

    protected abstract Assembly GetAssembly();
}