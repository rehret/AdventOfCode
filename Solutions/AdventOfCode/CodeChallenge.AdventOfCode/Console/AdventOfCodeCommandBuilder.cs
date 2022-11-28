namespace CodeChallenge.AdventOfCode.Console;

using System.CommandLine;
using System.CommandLine.Binding;
using System.Diagnostics;

using Autofac;

using CodeChallenge.AdventOfCode.IO;
using CodeChallenge.Core;
using CodeChallenge.Core.Console;

using Microsoft.Extensions.Logging;

internal sealed class AdventOfCodeCommandBuilder : AbstractCommandBuilder<AdventOfCodeChallengeSelection>
{
    protected override Command Command { get; }

    protected override BinderBase<AdventOfCodeChallengeSelection> Binder { get; }

    // NOTE: We're taking an ILifetimeScope instead of IAdventOfCodeInputWriter because we don't use the writer until the
    // Command is executed, so we want to use the correct scope
    public AdventOfCodeCommandBuilder(SolutionFactory solutionFactory, ILoggerFactory loggerFactory, ILifetimeScope lifetimeScope) : base(solutionFactory, loggerFactory)
    {
        var yearArgument = new Argument<int>("Year", "Advent of Code year, in the format 'YYYY'");
        var dayArgument = new Argument<int>("Day", "Day within the chosen year (1-25)");
        var puzzleArgument = new Argument<int>("Puzzle", "Puzzle for the given year and day (1-2)");
        Command = new Command("AdventOfCode", "Execute Advent of Code solutions");
        Command.AddAlias("advent");
        Command.AddArgument(yearArgument);
        Command.AddArgument(dayArgument);
        Command.AddArgument(puzzleArgument);

        var downloadCommand = new Command("fetch", "Fetch puzzle input for the given year and day");
        Command.AddCommand(downloadCommand);

        downloadCommand.SetHandler(async challengeSelection =>
        {
            var inputWriter = lifetimeScope.Resolve<IAdventOfCodeInputWriter>();
            await inputWriter.FetchRemoteInputAsync(challengeSelection).ConfigureAwait(false);
        }, new AdventOfCodeChallengeSelectionBinder(yearArgument, dayArgument));

        var openWebBrowserCommand = new Command("open", "Open the webpage for the given year and day in the default browser");
        Command.AddCommand(openWebBrowserCommand);

        openWebBrowserCommand.SetHandler(challengeSelection =>
        {
            Process.Start(new ProcessStartInfo(GetUriFromChallengeSelection(challengeSelection).ToString()) { UseShellExecute = true });
        }, new AdventOfCodeChallengeSelectionBinder(yearArgument, dayArgument));

        Binder = new AdventOfCodeChallengeSelectionBinder(yearArgument, dayArgument, puzzleArgument);
    }

    private static Uri GetUriFromChallengeSelection(AdventOfCodeChallengeSelection challengeSelection) =>
        new($"https://adventofcode.com/{challengeSelection.Year:0000}/day/{challengeSelection.Day:0}");

    private class AdventOfCodeChallengeSelectionBinder : BinderBase<AdventOfCodeChallengeSelection>
    {
        private readonly Argument<int> _yearArgument;
        private readonly Argument<int> _dayArgument;
        private readonly Argument<int>? _puzzleArgument;

        public AdventOfCodeChallengeSelectionBinder(Argument<int> yearArgument, Argument<int> dayArgument, Argument<int>? puzzleArgument = null)
        {
            _yearArgument = yearArgument;
            _dayArgument = dayArgument;
            _puzzleArgument = puzzleArgument;
        }

        protected override AdventOfCodeChallengeSelection GetBoundValue(BindingContext bindingContext)
        {
            return new AdventOfCodeChallengeSelection(
                bindingContext.ParseResult.GetValueForArgument(_yearArgument),
                bindingContext.ParseResult.GetValueForArgument(_dayArgument),
                _puzzleArgument != null ? bindingContext.ParseResult.GetValueForArgument(_puzzleArgument) : 0
            );
        }
    }
}