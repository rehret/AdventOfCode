namespace AdventOfCodeRunner;

using AdventOfCode;

using Autofac;
using Autofac.Core.Registration;

using Microsoft.Extensions.Logging;

internal class AdventOfCodeService
    : IAdventOfCodeService
{
    private readonly IInputReader _inputReader;
    private readonly ILifetimeScope _lifetimeScope;
    private readonly ILogger _logger;

    public AdventOfCodeService(IInputReader inputReader, ILifetimeScope lifetimeScope, ILoggerFactory loggerFactory)
    {
        _inputReader = inputReader;
        _lifetimeScope = lifetimeScope;
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
            solution = _lifetimeScope.ResolveKeyed<ISolution>((puzzleSelection.Year, puzzleSelection.Day, puzzleSelection.Puzzle));
        }
        catch (ComponentNotRegisteredException)
        {
            _logger.LogError("A Solution for {PuzzleSelectionYear:0000}/{PuzzleSelectionDay:00}/{PuzzleSelectionPuzzle:00} has not been registered", puzzleSelection.Year, puzzleSelection.Day, puzzleSelection.Puzzle);
            return;
        }

        IEnumerable<string> input;
        try
        {
            input = await _inputReader.GetInputAsync(puzzleSelection.Year, puzzleSelection.Day).ConfigureAwait(false);
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogError("Could not get input file for {PuzzleSelectionYear:0000}/{PuzzleSelectionDay:00}/{PuzzleSelectionPuzzle:00}: \'{TargetFileName}\'", puzzleSelection.Year, puzzleSelection.Day, puzzleSelection.Puzzle, ex.FileName);
            return;
        }
        var result = await solution.SolveAsync(input).ConfigureAwait(false);

        _logger.LogInformation("{Result}", result);
    }

    private void PrintUsage()
    {
        _logger.LogInformation("Usage: ./run <year>/<day>/<puzzle>");
    }
}