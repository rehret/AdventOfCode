namespace CodeChallenge.Core.Modules;

using System.CommandLine.Binding;

using Autofac;

using CodeChallenge.Core.CommandLine.Binding;

internal class AutofacBinderModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyOpenGenericTypes(ThisAssembly)
            .AssignableTo(typeof(AutofacBinder<>))
            .As(typeof(IValueDescriptor<>));
    }
}