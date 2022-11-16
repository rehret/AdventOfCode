namespace CodeChallenge.TomsDataOnion.IoC;

using System.Reflection;

using CodeChallenge.IoC;

internal class InputProviderModule : InputProviderAutoRegisteringModule
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}