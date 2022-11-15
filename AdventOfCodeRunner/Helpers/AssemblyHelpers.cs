namespace AdventOfCodeRunner.Helpers;

using System.Reflection;
using System.Text.RegularExpressions;

internal static class AssemblyHelpers
{
    private static readonly Regex ReferencedAssemblyPattern = new(@"AdventOfCode.*\.dll", RegexOptions.Compiled);

    public static Assembly[] GetReferencedAssemblies(bool includeCurrentAssembly = true)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();
        return Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories)
            .Where(filename => ReferencedAssemblyPattern.IsMatch(filename))
            .Select(Assembly.LoadFrom)
            .Where(assembly => includeCurrentAssembly || assembly != thisAssembly)
            .ToArray();
    }
}