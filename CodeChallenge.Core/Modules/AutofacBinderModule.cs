namespace CodeChallenge.Core.Modules;

using Autofac;

using CodeChallenge.Core.CommandLine.Binding;

internal class AutofacBinderModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyOpenGenericTypes(ThisAssembly)
            .As(typeof(IAutofacBinder<>));
    }
}