namespace CodeChallenge.Core.Modules;

using System.Reflection;

internal class InputProviderModule : InputProviderAutoRegisteringModule
{
    public InputProviderModule() : base(Assembly.GetExecutingAssembly()) { }
}