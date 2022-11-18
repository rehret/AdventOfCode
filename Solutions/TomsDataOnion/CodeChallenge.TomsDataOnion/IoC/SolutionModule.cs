namespace CodeChallenge.TomsDataOnion.IoC;

using System.Reflection;

using CodeChallenge.Core.IoC;

internal class SolutionModule : SolutionAutoRegisteringModule<TomsDataOnionSolutionAttribute>
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}