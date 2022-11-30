namespace CodeChallenge.Template.Solution.Modules;

using System.Reflection;

using CodeChallenge.Core.Modules;

internal class InputFilePathProviderModule : InputFilePathProviderAutoRegisteringModule
{
    public InputFilePathProviderModule() : base(Assembly.GetExecutingAssembly()) { }
}