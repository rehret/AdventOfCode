namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day09;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day09.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 9, 2)]
internal class Solution02 : AdventOfCodeSolution<IEnumerable<MoveInstruction>, int>
{
    private const int NumberOfKnots = 10;
    private const int DistanceThreshold = 1;

    public Solution02(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay09InputProvider())
    { }

    protected override int ComputeSolution(IEnumerable<MoveInstruction> input)
    {
        var knots = new Coordinate[NumberOfKnots];
        for (var i = 0; i < NumberOfKnots; i++)
        {
            knots[i] = new Coordinate(0, 0);
        }

        var tailVisitedPositions = new HashSet<Coordinate> { knots[NumberOfKnots - 1] };

        foreach (var instruction in input)
        {
            for (var i = 0; i < instruction.Amount; i++)
            {
                knots[0] = knots[0].Move(instruction.Direction);
                for (var knotIndex = 1; knotIndex < NumberOfKnots; knotIndex++)
                {
                    if (knots[knotIndex].DistanceTo(knots[knotIndex - 1]) > DistanceThreshold)
                    {
                        knots[knotIndex] = knots[knotIndex].MoveTowards(knots[knotIndex - 1]);
                    }
                }

                tailVisitedPositions.Add(knots[NumberOfKnots - 1]);
            }
        }

        return tailVisitedPositions.Count;
    }
}
