namespace AdventOfCodeRunner.IoC;

using System.Text.RegularExpressions;

using AdventOfCode;

using AdventOfCodeRunner.Helpers;

using Autofac;

using Module = Autofac.Module;

internal class SolutionsModule : Module
{
    private static readonly Regex SolutionTypeFullNameRegex = new(@"AdventOfCode(?<year>\d{4})\.Day(?<day>\d{2})\.Solution(?<solution>\d{2})", RegexOptions.Compiled);

    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = AssemblyHelpers.GetReferencedAssemblies();
        builder.RegisterAssemblyTypes(assemblies)
            .Keyed<ISolution>(type =>
            {
                var parts = GetSolutionTypeParts(type);
                if (parts == null)
                {
                    throw new Exception($"Found ISolution implementation with invalid fully-qualified name: '{type.FullName}'");
                }
                return (parts.Value.Year, parts.Value.Day, parts.Value.Solution);
            });
    }

    private static SolutionTypeParts? GetSolutionTypeParts(Type solutionType)
    {
        var fullTypeName = solutionType.FullName;
        var match = SolutionTypeFullNameRegex.Match(fullTypeName ?? string.Empty);

        if (!match.Success) return null;

        if (!int.TryParse(match.Groups["year"].Value, out var year)) return null;
        if (!int.TryParse(match.Groups["day"].Value, out var day)) return null;
        if (!int.TryParse(match.Groups["solution"].Value, out var solution)) return null;

        return new SolutionTypeParts
        {
            Year = year,
            Day = day,
            Solution = solution
        };
    }

    private struct SolutionTypeParts
    {
        public int Year;
        public int Day;
        public int Solution;
    }
}