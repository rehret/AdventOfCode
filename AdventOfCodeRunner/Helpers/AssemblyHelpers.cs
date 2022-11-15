namespace AdventOfCodeRunner.Helpers;

using System.Reflection;
using System.Text.RegularExpressions;

internal static class AssemblyHelpers
{
    private static readonly Regex ReferencedAssemblyPattern = new(@"AdventOfCode.*\.dll", RegexOptions.Compiled);

    private static Assembly[]? _referencedAssemblies;
    private static readonly object ReferencedAssembliesLock = new();

    public static Assembly[] GetReferencedAssemblies(bool includeCurrentAssembly = true)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        lock (ReferencedAssembliesLock)
        {
            _referencedAssemblies ??= Directory
                .EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories)
                .Where(filename => ReferencedAssemblyPattern.IsMatch(filename))
                .Select(Assembly.LoadFrom)
                .ToArray();
        }
        return _referencedAssemblies.Where(assembly => includeCurrentAssembly || assembly != thisAssembly).ToArray();
    }
}