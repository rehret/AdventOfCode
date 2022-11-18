namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Modules;

using System.Reflection;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.Modules;

internal class SolutionModule : SolutionAutoRegisteringModule<AdventOfCodeSolutionAttribute>
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}