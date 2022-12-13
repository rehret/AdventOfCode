namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day13;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day13.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 13, 1)]
internal class Solution01 : AdventOfCodeSolution<IEnumerable<(PacketData Left, PacketData Right)>, int>
{
    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay13InputProvider())
    { }

    protected override int ComputeSolution(IEnumerable<(PacketData Left, PacketData Right)> input)
    {
        var sum = 0;
        var index = 1;
        foreach (var pair in input)
        {
            if (pair.Left.CompareTo(pair.Right) < 0)
            {
                sum += index;
            }

            index++;
        }

        return sum;
    }
}
