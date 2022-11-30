namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Modules;

using System.Reflection;

using CodeChallenge.Core.Modules;

internal class InputProviderModule : InputProviderAutoRegisteringModule
{
    public InputProviderModule() : base(Assembly.GetExecutingAssembly()) { }
}