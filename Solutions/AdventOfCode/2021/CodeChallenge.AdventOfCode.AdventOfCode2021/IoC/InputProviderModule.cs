namespace CodeChallenge.AdventOfCode.AdventOfCode2021.IoC;

using System.Reflection;

using CodeChallenge.IoC;

internal class InputProviderModule : InputProviderAutoRegisteringModule
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}