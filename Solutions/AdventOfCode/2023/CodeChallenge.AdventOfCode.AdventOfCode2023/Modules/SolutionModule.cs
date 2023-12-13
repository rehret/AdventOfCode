namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Modules;

using System.Reflection;

using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.Modules;

internal class SolutionModule()
    : SolutionAutoRegisteringModule<AdventOfCodeSolutionAttribute>(Assembly.GetExecutingAssembly());