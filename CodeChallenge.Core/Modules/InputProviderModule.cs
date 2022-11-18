namespace CodeChallenge.Core.Modules;

using System.Reflection;

internal class InputProviderModule : InputProviderAutoRegisteringModule
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}