namespace AdventOfCode2022.Day01;

using Microsoft.Extensions.Logging;

internal class Solution02 : AbstractSolution<string, int>
{
    public Solution02(IInputReader inputReader, IInputProcessor<string> inputProcessor, ILoggerFactory loggerFactory)
        : base(inputReader, inputProcessor, loggerFactory)
    { }

    public override Task<int> ComputeSolutionAsync(IEnumerable<string> input)
    {
        throw new NotImplementedException();
    }
}