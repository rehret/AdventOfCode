namespace CodeChallenge.Template.Solution;

using System.Reflection;

using CodeChallenge.IoC;

internal class SolutionModule : SolutionAutoRegisteringModule<SolutionTemplateSolutionAttribute>
{
    protected override Assembly GetAssembly() => Assembly.GetExecutingAssembly();
}