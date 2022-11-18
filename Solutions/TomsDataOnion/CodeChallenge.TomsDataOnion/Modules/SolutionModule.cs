namespace CodeChallenge.TomsDataOnion.Modules;

using System.Reflection;

using CodeChallenge.Core.Modules;
using CodeChallenge.TomsDataOnion.Attributes;

internal class SolutionModule : SolutionAutoRegisteringModule<TomsDataOnionSolutionAttribute>
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}