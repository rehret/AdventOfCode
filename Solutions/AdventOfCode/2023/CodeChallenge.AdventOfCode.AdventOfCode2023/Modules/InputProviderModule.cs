namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Modules;

using System.Reflection;

using CodeChallenge.Core.Modules;

internal class InputProviderModule() : InputProviderAutoRegisteringModule(Assembly.GetExecutingAssembly());