namespace CodeChallenge.Runner;

using Autofac.Core.Registration;

using CodeChallenge;

using Microsoft.Extensions.Logging;

internal class CodeChallengeService
    : ICodeChallengeService
{
    private readonly IChallengeSelectionParser _parser;
    private readonly SolutionFactory _solutionFactory;
    private readonly ILogger _logger;

    public CodeChallengeService(IChallengeSelectionParser parser, SolutionFactory solutionFactory, ILoggerFactory loggerFactory)
    {
        _parser = parser;
        _solutionFactory = solutionFactory;
        _logger = loggerFactory.CreateLogger<CodeChallengeService>();
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var args = Environment.GetCommandLineArgs();

        if (!_parser.TryParse(args, out var puzzleSelection))
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

    private void PrintUsage()
    {
        const int sizeOfIndent = 2;
        const int sizeOfGapBetweenColumns = 2;

        const string challengeTypeHeader = "Challenge Type";
        const string selectorHeader = "Selector";
        const string aliasHeader = "Alias";

        const string aliasSeparator = ", ";
        const string singleSpace = " ";

        var usages = _parser.GetAllChallengeUsages().ToArray();
        var maxChallengeNameLength = usages.Select(x => x.ChallengeName.Length).Append(challengeTypeHeader.Length).Max();
        var maxSelectorLength = usages.Select(x => x.Usage.Length).Append(selectorHeader.Length).Max();
        var maxAliasLength = usages.Select(x => x.Aliases.Select(alias => alias.Length).Sum() + x.Aliases.Length - 1).Append(aliasHeader.Length).Max();

        var header = string.Join("", Enumerable.Repeat(singleSpace, sizeOfIndent)
            .Append(challengeTypeHeader)
            .Concat(Enumerable.Repeat(singleSpace, maxChallengeNameLength - challengeTypeHeader.Length))
            .Concat(Enumerable.Repeat(singleSpace, sizeOfGapBetweenColumns))
            .Append(selectorHeader)
            .Concat(Enumerable.Repeat(singleSpace, maxSelectorLength - selectorHeader.Length))
            .Concat(Enumerable.Repeat(singleSpace, sizeOfGapBetweenColumns))
            .Append(aliasHeader));

        var divider = string.Join("", Enumerable.Repeat(singleSpace, sizeOfIndent)
            .Concat(Enumerable.Repeat("-", maxChallengeNameLength))
            .Concat(Enumerable.Repeat(singleSpace, sizeOfGapBetweenColumns))
            .Concat(Enumerable.Repeat("-", maxSelectorLength))
            .Concat(Enumerable.Repeat(singleSpace, sizeOfGapBetweenColumns))
            .Concat(Enumerable.Repeat("-", maxAliasLength)));

        var usageLines = usages.Select(x =>
            string.Join("", Enumerable.Repeat(singleSpace, sizeOfIndent)
                .Append(x.ChallengeName)
                .Concat(Enumerable.Repeat(singleSpace, maxChallengeNameLength - x.ChallengeName.Length))
                .Concat(Enumerable.Repeat(singleSpace, sizeOfGapBetweenColumns))
                .Append(x.Usage)
                .Concat(Enumerable.Repeat(singleSpace, maxSelectorLength - x.Usage.Length))
                .Concat(Enumerable.Repeat(singleSpace, sizeOfGapBetweenColumns))
                .Append(string.Join(aliasSeparator, x.Aliases))));

        Console.WriteLine();
        Console.WriteLine("Usage: ./run <puzzle selector>");
        Console.WriteLine();
        Console.WriteLine(header);
        Console.WriteLine(divider);
        foreach (var usageLine in usageLines)
        {
            Console.WriteLine(usageLine);
        }
        Console.WriteLine();
    }
}