namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day09;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day09.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 9, 1)]
internal class Solution01 : AdventOfCodeSolution<IEnumerable<MoveInstruction>, int>
{
    private const int DistanceThreshold = 1;

    public Solution01(IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder)
        : base(inputProviderBuilder.BuildDay09InputProvider())
    { }

    protected override int ComputeSolution(IEnumerable<MoveInstruction> input)
    {
        var tailVisitedPositions = new HashSet<Coordinate>();
        var head = new Coordinate(0, 0);
        var tail = new Coordinate(0, 0);

        tailVisitedPositions.Add(tail);

        foreach (var instruction in input)
        {
            for (var i = 0; i < instruction.Amount; i++)
            {
                head = head.Move(instruction.Direction);
                if (tail.DistanceTo(head) > DistanceThreshold)
                {
                    tail = tail.MoveTowards(head);
                    tailVisitedPositions.Add(tail);
                }
            }
        }

        return tailVisitedPositions.Count;
    }
}
