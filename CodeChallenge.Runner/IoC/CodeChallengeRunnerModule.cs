namespace CodeChallenge.Runner.IoC;

using System.Reflection;
using System.Text.RegularExpressions;

using Autofac;

using Module = Autofac.Module;

internal class AdventOfCodeRunnerModule : Module
{
    private static readonly Regex ReferencedAssemblyPattern = new(@"CodeChallenge[^\\/]*\.dll", RegexOptions.Compiled);

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CodeChallengeService>().As<ICodeChallengeService>();

        var assemblies = Directory
            .EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories)
            .Where(filename => ReferencedAssemblyPattern.IsMatch(filename))
            .Select(Assembly.LoadFrom)
            .Where(assembly => assembly != ThisAssembly)
            .ToArray();
        builder.RegisterAssemblyModules(assemblies);
    }
}