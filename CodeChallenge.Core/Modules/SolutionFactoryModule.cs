namespace CodeChallenge.Core.Modules;

using Autofac;

using CodeChallenge.Core;

internal class SolutionFactoryModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register<SolutionFactory>(ctx =>
        {
            var lifetimeScope = ctx.Resolve<ILifetimeScope>();
            return puzzleSelection => lifetimeScope.ResolveKeyed<ISolution>(puzzleSelection);
        });
    }
}