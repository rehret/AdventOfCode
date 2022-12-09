namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day09;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day09.Models;
using CodeChallenge.Core.IO;

internal abstract class AbstractDay09Solution : AdventOfCodeSolution<IEnumerable<MoveInstruction>, int>
{
    private const int DistanceThreshold = 1;

    private readonly int _numberOfKnots;

    protected AbstractDay09Solution(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder, int numberOfKnots)
        : base(inputProviderBuilder.BuildDay09InputProvider())
    {
        _numberOfKnots = numberOfKnots;
    }

    protected override int ComputeSolution(IEnumerable<MoveInstruction> input)
    {
        var knots = new Coordinate[_numberOfKnots];
        for (var i = 0; i < _numberOfKnots; i++)
        {
            knots[i] = new Coordinate(0, 0);
        }

        var tailVisitedPositions = new HashSet<Coordinate>
        {
            knots[_numberOfKnots - 1]
        };

        foreach (var instruction in input)
        {
            for (var i = 0; i < instruction.Amount; i++)
            {
                knots[0] = knots[0].Move(instruction.Direction);
                MoveTrailingKnots(knots);

                tailVisitedPositions.Add(knots[_numberOfKnots - 1]);
            }
        }

        return tailVisitedPositions.Count;
    }

    private void MoveTrailingKnots(IList<Coordinate> knots)
    {
        for (var knotIndex = 1; knotIndex < _numberOfKnots; knotIndex++)
        {
            if (knots[knotIndex].DistanceTo(knots[knotIndex - 1]) > DistanceThreshold)
            {
                knots[knotIndex] = knots[knotIndex].MoveTowards(knots[knotIndex - 1]);
            }
        }
    }
}