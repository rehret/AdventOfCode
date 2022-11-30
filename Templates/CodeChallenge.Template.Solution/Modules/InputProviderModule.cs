namespace CodeChallenge.Template.Solution.Modules;

using System.Reflection;

using CodeChallenge.Core.Modules;

internal class InputProviderModule : InputProviderAutoRegisteringModule
{
    public InputProviderModule() : base(Assembly.GetExecutingAssembly()) { }
}