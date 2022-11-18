namespace CodeChallenge.Core.Modules;

using System.Reflection;

internal class InputReaderModule : InputReaderAutoRegisteringModule
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}