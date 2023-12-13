namespace CodeChallenge.TomsDataOnion.Modules;

using System.Reflection;

using CodeChallenge.Core.Modules;

internal class InputProviderModule() : InputProviderAutoRegisteringModule(Assembly.GetExecutingAssembly());