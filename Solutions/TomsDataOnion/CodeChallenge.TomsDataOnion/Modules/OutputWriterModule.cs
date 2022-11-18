namespace CodeChallenge.TomsDataOnion.Modules;

using Autofac;

using CodeChallenge.TomsDataOnion.IO;

internal class OutputWriterModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<TomsDataOnionOutputWriter>().As<ITomsDataOnionOutputWriter>();
    }
}