namespace CodeChallenge.TomsDataOnion.IoC;

using Autofac;

internal class TomsDataOnionModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Ascii85Decoder>().As<IAscii85Decoder>();
    }
}