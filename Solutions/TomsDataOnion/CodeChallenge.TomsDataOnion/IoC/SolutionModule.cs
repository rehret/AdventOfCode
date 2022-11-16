namespace CodeChallenge.TomsDataOnion.IoC;

using System.Reflection;

using CodeChallenge.IoC;

internal class SolutionModule : SolutionAutoRegisteringModule<TomsDataOnionSolutionAttribute>
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}