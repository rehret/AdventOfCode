namespace CodeChallenge.AdventOfCode.Modules;

using System.Reflection;

using CodeChallenge.Core.Modules;

internal class InputFilePathProviderModule : InputFilePathProviderAutoRegisteringModule
{
    public InputFilePathProviderModule() : base(Assembly.GetExecutingAssembly()) { }
}