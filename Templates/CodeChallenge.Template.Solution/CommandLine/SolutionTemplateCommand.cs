namespace CodeChallenge.Template.Solution.CommandLine;

using System.CommandLine;
using System.CommandLine.Binding;

using CodeChallenge.Core;
using CodeChallenge.Core.CommandLine;

using Microsoft.Extensions.Logging;

internal class SolutionTemplateCommand : AbstractCodeChallengeCommand<SolutionTemplateChallengeSelection>
{
    public SolutionTemplateCommand(IValueDescriptor<SolutionFactory> solutionFactoryBinder, IValueDescriptor<ILoggerFactory> loggerFactoryBinder) : base("SolutionTemplate", "Executes SolutionTemplate solutions")
    {
        this.SetHandler(ExecuteSolutionAsync,
            new SolutionTemplateChallengeSelectionBinder(),
            solutionFactoryBinder,
            loggerFactoryBinder);
    }

    private class SolutionTemplateChallengeSelectionBinder : BinderBase<SolutionTemplateChallengeSelection>
    {
        protected override SolutionTemplateChallengeSelection GetBoundValue(BindingContext bindingContext)
        {
            throw new NotImplementedException();
        }
    }
}