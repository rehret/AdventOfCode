namespace CodeChallenge.Template.Solution.IoC;

using System.Reflection;

using CodeChallenge.Core.IoC;

internal class SolutionModule : SolutionAutoRegisteringModule<SolutionTemplateSolutionAttribute>
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}