namespace CodeChallenge.IoC;

using System.Reflection;

internal class InputProviderModule : InputProviderAutoRegisteringModule
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}