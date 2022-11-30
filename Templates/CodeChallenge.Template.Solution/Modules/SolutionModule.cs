namespace CodeChallenge.Template.Solution.Modules;

using System.Reflection;

using CodeChallenge.Core.Modules;
using CodeChallenge.Template.Solution.Attributes;

internal class SolutionModule : SolutionAutoRegisteringModule<SolutionTemplateSolutionAttribute>
{
    public SolutionModule() : base(Assembly.GetExecutingAssembly()) { }
}