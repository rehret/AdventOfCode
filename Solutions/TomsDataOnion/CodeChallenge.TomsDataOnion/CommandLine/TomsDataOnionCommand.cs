namespace CodeChallenge.TomsDataOnion.CommandLine;

using System.CommandLine;
using System.CommandLine.Binding;

using CodeChallenge.Core;
using CodeChallenge.Core.CommandLine;

using Microsoft.Extensions.Logging;

internal class TomsDataOnionCommand : AbstractCodeChallengeCommand<TomsDataOnionChallengeSelection>
{
    public TomsDataOnionCommand(IValueDescriptor<SolutionFactory> solutionFactoryBinder, IValueDescriptor<ILoggerFactory> loggerFactoryBinder) : base("TomsDataOnion", "Execute Tom's Data Onion solutions")
    {
        var challengeSelectionArgument = new Argument<int>("Layer selection", "Layer selection as an integer");

        AddAlias("toms");
        AddArgument(challengeSelectionArgument);

        this.SetHandler(ExecuteSolutionAsync,
            new TomsDataOnionChallengeSelectionBinder(challengeSelectionArgument),
            solutionFactoryBinder,
            loggerFactoryBinder);
    }

    private class TomsDataOnionChallengeSelectionBinder : BinderBase<TomsDataOnionChallengeSelection>
    {
        private readonly Argument<int> _challengeSelection;

        public TomsDataOnionChallengeSelectionBinder(Argument<int> challengeSelection)
        {
            _challengeSelection = challengeSelection;
        }

        protected override TomsDataOnionChallengeSelection GetBoundValue(BindingContext bindingContext)
        {
            return new TomsDataOnionChallengeSelection(bindingContext.ParseResult.GetValueForArgument(_challengeSelection));
        }
    }
}