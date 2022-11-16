namespace CodeChallenge.TomsDataOnion.IoC;

using Autofac;

internal class OutputWriterModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TomsDataOnionOutputWriter>().As<ITomsDataOnionOutputWriter>();
    }
}