namespace CodeChallenge.Runner.IoC;

using Autofac;

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