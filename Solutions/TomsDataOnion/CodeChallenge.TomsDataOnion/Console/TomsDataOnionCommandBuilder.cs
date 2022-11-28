namespace CodeChallenge.TomsDataOnion.Console;

using System.CommandLine;
using System.CommandLine.Binding;

using CodeChallenge.Core;
using CodeChallenge.Core.Console;

using Microsoft.Extensions.Logging;

internal sealed class TomsDataOnionCommandBuilder : AbstractCommandBuilder<TomsDataOnionChallengeSelection>
{
    protected override Command Command { get; }
    protected override BinderBase<TomsDataOnionChallengeSelection> Binder { get; }

    public TomsDataOnionCommandBuilder(SolutionFactory solutionFactory, ILoggerFactory loggerFactory) : base(solutionFactory, loggerFactory)
    {
        var challengeSelectionArgument = new Argument<int>("Layer selection", "Layer selection as an integer");

        Command = new Command("TomsDataOnion", "Execute Tom's Data Onion solutions");
        Command.AddAlias("toms");
        Command.AddArgument(challengeSelectionArgument);

        Binder = new TomsDataOnionChallengeSelectionBinder(challengeSelectionArgument);
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