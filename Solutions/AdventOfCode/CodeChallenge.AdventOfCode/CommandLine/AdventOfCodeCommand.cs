namespace CodeChallenge.AdventOfCode.CommandLine;

using System.CommandLine;
using System.CommandLine.Binding;
using System.CommandLine.Parsing;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using CodeChallenge.AdventOfCode.IO;
using CodeChallenge.Core;
using CodeChallenge.Core.CommandLine;
using CodeChallenge.Core.CommandLine.Binding;

using Microsoft.Extensions.Logging;

internal class AdventOfCodeCommand : AbstractCodeChallengeCommand<AdventOfCodeChallengeSelection>
{
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameterInConstructor")]
    public AdventOfCodeCommand(IAutofacBinder<SolutionFactory> solutionFactoryBinder, IAutofacBinder<ILoggerFactory> loggerFactoryBinder, IAutofacBinder<IAdventOfCodeInputWriter> inputWriterBinder)
        : base("AdventOfCode", "Executes Advent of Code solutions")
    {
        AddAlias("advent");
        var yearArgument = new Argument<int>("Year", "Advent of Code year, in the format 'yyyy'");
        var dayArgument = new Argument<int>("Day", GetIntArgumentParser("day", 1, 25), false, "Day within the chosen year [1, 25]");
        var puzzleArgument = new Argument<int>("Puzzle", GetIntArgumentParser("puzzle", 1, 2), false, "Puzzle for the given year and day [1, 2]");
        AddArgument(yearArgument);
        AddArgument(dayArgument);
        AddArgument(puzzleArgument);

        this.SetHandler(ExecuteSolutionAsync,
            new AdventOfCodeChallengeSelectionBinder(yearArgument, dayArgument, puzzleArgument),
            solutionFactoryBinder,
            loggerFactoryBinder);

        AddCommand(BuildDownloadCommand(yearArgument, dayArgument, inputWriterBinder));
        AddCommand(BuildOpenCommand(yearArgument, dayArgument));
    }

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

    private static Command BuildDownloadCommand(Argument<int> yearArgument, Argument<int> dayArgument, IValueDescriptor<IAdventOfCodeInputWriter> inputWriterBinder)
    {
        var downloadCommand = new Command("fetch", "Fetch puzzle input for the given year and day");
        downloadCommand.SetHandler(async (challengeSelection, inputWriter) =>
        {
            await inputWriter.FetchRemoteInputAsync(challengeSelection).ConfigureAwait(false);
        }, new AdventOfCodeChallengeSelectionBinder(yearArgument, dayArgument), inputWriterBinder);

        return downloadCommand;
    }

    private static Command BuildOpenCommand(Argument<int> yearArgument, Argument<int> dayArgument)
    {
        var openWebBrowserCommand = new Command("open", "Open the webpage for the given year and day in the default browser");
        openWebBrowserCommand.SetHandler(challengeSelection =>
        {
            Process.Start(new ProcessStartInfo(AdventOfCodeResourcePathBuilder.GetWebPageUri(challengeSelection).ToString()) { UseShellExecute = true });
        }, new AdventOfCodeChallengeSelectionBinder(yearArgument, dayArgument));

        return openWebBrowserCommand;
    }

    private static ParseArgument<int> GetIntArgumentParser(string argumentName, int minValue, int maxValue)
    {
        return result =>
        {
            if (result.Tokens.Count == 0)
            {
                result.ErrorMessage = $"Missing {argumentName} argument";
                return 0;
            }

            var token = result.Tokens.Single().Value;
            if (!int.TryParse(token, out var intValue))
            {
                result.ErrorMessage = $"Could not parse {argumentName} argument as an integer";
                return 0;
            }

            if (intValue >= minValue && intValue <= maxValue) return intValue;
            result.ErrorMessage = $"{argumentName[..1].ToUpper() + argumentName[1..]} argument must be in the range [{minValue}, {maxValue}]";
            return 0;
        };
    }
}