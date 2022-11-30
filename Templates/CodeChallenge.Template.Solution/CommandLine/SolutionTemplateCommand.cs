namespace CodeChallenge.Template.Solution.CommandLine;

using System.CommandLine;
using System.CommandLine.Binding;
using System.Diagnostics.CodeAnalysis;

using CodeChallenge.Core;
using CodeChallenge.Core.CommandLine;
using CodeChallenge.Core.CommandLine.Binding;

using Microsoft.Extensions.Logging;

internal class SolutionTemplateCommand : AbstractCodeChallengeCommand<SolutionTemplateChallengeSelection>
{
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameterInConstructor")]
    public SolutionTemplateCommand(IAutofacBinder<SolutionFactory> solutionFactoryBinder, IAutofacBinder<ILoggerFactory> loggerFactoryBinder)
        : base("SolutionTemplate", "Executes SolutionTemplate solutions")
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