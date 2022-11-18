namespace CodeChallenge.Template.Solution.Modules;

using System.Reflection;

using CodeChallenge.Core.Modules;

internal class InputReaderModule : InputReaderAutoRegisteringModule
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}