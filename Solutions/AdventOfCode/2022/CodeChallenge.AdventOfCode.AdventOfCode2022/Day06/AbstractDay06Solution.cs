namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day06;

using CodeChallenge.Core.IO;

internal class AbstractDay06Solution : AdventOfCodeSolution<string, int>
{
    private readonly int _bufferSize;

    protected AbstractDay06Solution(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder, int bufferSize)
        : base(inputProviderBuilder.ReadAllInput(true).AsString().Build())
    {
        _bufferSize = bufferSize;
    }

    protected override int ComputeSolution(string input)
    {
        for (var i = 0; i < input.Length - _bufferSize; i++)
        {
            if (input[i..(i + _bufferSize)].Distinct().Count() == _bufferSize)
            {
                return i + _bufferSize;
            }
        }

        return -1;
    }
}
