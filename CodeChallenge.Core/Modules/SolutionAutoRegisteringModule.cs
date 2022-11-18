namespace CodeChallenge.Core.Modules;

using System.Reflection;

using Autofac;

using CodeChallenge.Core.Attributes;

using Module = Autofac.Module;

public abstract class SolutionAutoRegisteringModule<T> : Module
    where T : SolutionAttribute
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(GetAssembly())
            .Where(type => type.GetCustomAttribute<T>(true) != null)
            .Keyed<ISolution>(type => type.GetCustomAttribute<T>()!.ToChallengeSelection());
    }

    protected abstract Assembly GetAssembly();
}