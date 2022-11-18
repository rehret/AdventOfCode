namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Modules;

using System.Reflection;

using CodeChallenge.Core.Modules;

internal class InputReaderModule : InputReaderAutoRegisteringModule
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}