namespace CodeChallenge.Template.Solution.IoC;

using System.Reflection;

using CodeChallenge.Core.IoC;

internal class InputProviderModule : InputProviderAutoRegisteringModule
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}