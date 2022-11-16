namespace AdventOfCodeRunner.IoC;

using System.Reflection;

using AdventOfCode;

using AdventOfCodeRunner.Helpers;

using Autofac;

using Module = Autofac.Module;

public delegate ISolution SolutionFactory(PuzzleSelection puzzleSelection);

internal class SolutionsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register<SolutionFactory>(ctx =>
        {
            var lifetimeScope = ctx.Resolve<ILifetimeScope>();
            return puzzleSelection => lifetimeScope.ResolveKeyed<ISolution>(puzzleSelection);
        });

        builder.RegisterAssemblyTypes(AssemblyHelpers.GetReferencedAssemblies())
            .Where(type => type.GetCustomAttribute<SolutionAttribute>() != null)
            .Keyed<ISolution>(type =>
            {
                var puzzleSelectorAttribute = type.GetCustomAttribute<SolutionAttribute>()!;
                return new PuzzleSelection(puzzleSelectorAttribute.Year, puzzleSelectorAttribute.Day, puzzleSelectorAttribute.Puzzle);
            });
    }
}