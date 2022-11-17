namespace CodeChallenge.Runner;

using Autofac.Core.Registration;

using CodeChallenge;

using Microsoft.Extensions.Logging;

internal class CodeChallengeService
    : ICodeChallengeService
{
    private readonly SolutionFactory _solutionFactory;
    private readonly ILogger _logger;

    public CodeChallengeService(SolutionFactory solutionFactory, ILoggerFactory loggerFactory)
    {
        _solutionFactory = solutionFactory;
        _logger = loggerFactory.CreateLogger<CodeChallengeService>();
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var args = Environment.GetCommandLineArgs();

        if (!ChallengeSelectionParser.TryParse(args, out var puzzleSelection))
        {
            PrintUsage();
            return;
        }

        ISolution solution;
        try
        {
            solution = _solutionFactory(puzzleSelection);
        }
        catch (ComponentNotRegisteredException)
        {
            _logger.LogError("A Solution for {PuzzleSelection} has not been registered", puzzleSelection);
            return;
        }

        try
        {
            var result = await solution.SolveAsync().ConfigureAwait(false);
            Console.WriteLine(result);
        }
        catch (Exception ex) when (ex is FileNotFoundException or DirectoryNotFoundException)
        {
            _logger.LogError(ex, "Could not load input file for {PuzzleSelection}", puzzleSelection);
        }
    }

    private static void PrintUsage()
    {
        Console.WriteLine();
        Console.WriteLine("Usage: ./run <puzzle selector>");
        Console.WriteLine();
        Console.WriteLine("  Puzzle Type         Selector");
        Console.WriteLine("  ----------------    -------------------------------------------");
        Console.WriteLine("  Advent Of Code      <AdventOfCode|advent>/<year>/<day>/<puzzle>");
        Console.WriteLine("  Tom's Data Onion    <CodeChallenge.TomsDataOnion|Toms|DataOnion>/<layer>");
        Console.WriteLine();
    }
}