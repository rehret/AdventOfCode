namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Modules;

using System.Reflection;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.Modules;

internal class SolutionModule : SolutionAutoRegisteringModule<AdventOfCodeSolutionAttribute>
{
    public SolutionModule() : base(Assembly.GetExecutingAssembly()) { }
}