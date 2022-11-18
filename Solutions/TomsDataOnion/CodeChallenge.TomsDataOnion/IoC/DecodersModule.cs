namespace CodeChallenge.TomsDataOnion.IoC;

using Autofac;

using CodeChallenge.TomsDataOnion.Decoders;

internal class DecodersModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<Ascii85Decoder>().As<IAscii85Decoder>();
    }
}