namespace CodeChallenge.Template.Solution.Console;

using System.CommandLine;
using System.CommandLine.Binding;

using CodeChallenge.Core;
using CodeChallenge.Core.Console;

using Microsoft.Extensions.Logging;

internal sealed class SolutionTemplateCommandBuilder : AbstractCommandBuilder<SolutionTemplateChallengeSelection>
{
    protected override Command Command { get; }
    protected override BinderBase<SolutionTemplateChallengeSelection> Binder { get; }

    public SolutionTemplateCommandBuilder(SolutionFactory solutionFactory, ILoggerFactory loggerFactory) : base(solutionFactory, loggerFactory)
    {
        Command = new Command("SolutionTemplate", "Execute SolutionTemplate solutions");
        Binder = new SolutionTemplateChallengeSelectionBinder();
        throw new NotImplementedException();
    }

    private class SolutionTemplateChallengeSelectionBinder : BinderBase<SolutionTemplateChallengeSelection>
    {
        protected override SolutionTemplateChallengeSelection GetBoundValue(BindingContext bindingContext)
        {
            throw new NotImplementedException();
        }
    }
}