namespace AdventOfCodeRunner.IoC;

using System.Reflection;
using System.Text.RegularExpressions;

using Autofac;

using Module = Autofac.Module;

internal class AdventOfCodeRunnerModule : Module
{
    private static readonly Regex ReferencedAssemblyPattern = new Regex(@"AdventOfCode.*\.dll", RegexOptions.Compiled);

    protected override void Load(ContainerBuilder builder)
    {
        var assemblies = Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories)
            .Where(filename => ReferencedAssemblyPattern.IsMatch(filename))
            .Select(Assembly.LoadFrom)
            .Where(assembly => assembly != ThisAssembly)
            .ToArray();

        builder.RegisterAssemblyModules(assemblies);
    }
}