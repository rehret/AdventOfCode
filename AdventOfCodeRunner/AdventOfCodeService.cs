namespace AdventOfCodeRunner;

using AdventOfCode;

using AdventOfCodeRunner.IoC;

using Autofac.Core.Registration;

using Microsoft.Extensions.Logging;

internal class AdventOfCodeService
    : IAdventOfCodeService
{
    private readonly SolutionFactory _solutionFactory;
    private readonly ILogger _logger;

    public AdventOfCodeService(SolutionFactory solutionFactory, ILoggerFactory loggerFactory)
    {
        _solutionFactory = solutionFactory;
        _logger = loggerFactory.CreateLogger<AdventOfCodeService>();
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var args = Environment.GetCommandLineArgs();

        PuzzleSelection puzzleSelection;
        try
        {
            puzzleSelection = PuzzleSelection.FromArguments(args);
        }
        catch (PuzzleSelection.InvalidPuzzleSelectionException)
        {
            PrintUsage();
            return;
        }
        catch (PuzzleSelection.PuzzleSelectionParseException ex)
        {
            PrintUsage();
            _logger.LogError(ex, "Could not parse puzzle selection");
            return;
        }

        ISolution solution;
        try
        {
            solution = _solutionFactory(puzzleSelection);
        }
        catch (ComponentNotRegisteredException)
        {
            _logger.LogError("A Solution for {PuzzleSelectionYear:0000}/{PuzzleSelectionDay:00}/{PuzzleSelectionPuzzle:00} has not been registered", puzzleSelection.Year, puzzleSelection.Day, puzzleSelection.Puzzle);
            return;
        }

        await solution.SolveAsync().ConfigureAwait(false);
    }

    private void PrintUsage()
    {
        _logger.LogInformation("Usage: ./run <year>/<day>/<puzzle>");
    }
}