namespace AdventOfCodeRunner;

using AdventOfCode;

using Autofac;
using Autofac.Core.Registration;

using Microsoft.Extensions.Hosting;

internal class AdventOfCodeService : IHostedService
{
    private readonly IHostApplicationLifetime _hostLifetime;
    private readonly IInputReader _inputReader;
    private readonly ILifetimeScope _lifetimeScope;

    public AdventOfCodeService(IHostApplicationLifetime hostLifetime, IInputReader inputReader, ILifetimeScope lifetimeScope)
    {
        _hostLifetime = hostLifetime;
        _inputReader = inputReader;
        _lifetimeScope = lifetimeScope;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
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
            _hostLifetime.StopApplication();
            return;
        }
        catch (PuzzleSelection.PuzzleSelectionParseException ex)
        {
            PrintUsage();
            Console.WriteLine(ex.Message);
            _hostLifetime.StopApplication();
            return;
        }

        ISolution solution;
        try
        {
            solution = _lifetimeScope.ResolveKeyed<ISolution>((puzzleSelection.Year, puzzleSelection.Day, puzzleSelection.Puzzle));
        }
        catch (ComponentNotRegisteredException)
        {
            Console.WriteLine($"A Solution for {puzzleSelection.Year:0000}/{puzzleSelection.Day:00}/{puzzleSelection.Puzzle:00} has not been registered");
            _hostLifetime.StopApplication();
            return;
        }

        IEnumerable<string> input;
        try
        {
            input = await _inputReader.GetInputAsync(puzzleSelection.Year, puzzleSelection.Day).ConfigureAwait(false);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Could not get input file for {puzzleSelection.Year:0000}/{puzzleSelection.Day:00}/{puzzleSelection.Puzzle:00}: '{ex.FileName}'");
            _hostLifetime.StopApplication();
            return;
        }
        var result = await solution.SolveAsync(input).ConfigureAwait(false);

        Console.WriteLine(result);
        _hostLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private static void PrintUsage()
    {
        Console.WriteLine("Usage: ./run <year>/<day>/<puzzle>");
    }
}