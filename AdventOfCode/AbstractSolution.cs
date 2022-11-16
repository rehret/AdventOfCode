namespace AdventOfCode;

using Microsoft.Extensions.Logging;

public abstract class AbstractSolution<TInput, TResult> : ISolution
{
    private readonly IInputReader _inputReader;
    private readonly IInputProcessor<TInput> _inputProcessor;
    private readonly ILogger _logger;

    protected AbstractSolution(IInputReader inputReader, IInputProcessor<TInput> inputProcessor, ILoggerFactory loggerFactory)
    {
        _inputReader = inputReader;
        _inputProcessor = inputProcessor;
        _logger = loggerFactory.CreateLogger(GetType());
    }

    public async Task SolveAsync()
    {
        var input = await _inputReader.GetInputAsync().ConfigureAwait(false);
        var processedInput = _inputProcessor.Process(input);
        var result = await ComputeSolutionAsync(processedInput).ConfigureAwait(false);
        _logger.LogInformation("{Result}", result);
    }

    public abstract Task<TResult> ComputeSolutionAsync(IEnumerable<TInput> input);
}