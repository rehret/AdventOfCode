namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day15;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day15.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 15, 1)]
internal class Solution01 : AdventOfCodeSolution<IEnumerable<Sensor>, int>
{
    private readonly int _targetRow;

    public Solution01(
        IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder,
        int targetRow = 2000000
    )
        : base(inputProviderBuilder.BuildDay15InputProvider())
    {
        _targetRow = targetRow;
    }

    protected override int ComputeSolution(IEnumerable<Sensor> input)
    {
        var sensors = input.ToList();
        var ranges = sensors
            .Select(sensor =>
            {
                var distanceToBeacon = sensor.Position.GetDistanceTo(sensor.DetectedBeacon.Position);
                var distanceToTargetRow = sensor.Position.GetDistanceTo(sensor.Position with { Y = _targetRow });
                var horizontalCoverage = distanceToBeacon - distanceToTargetRow;

                return horizontalCoverage <= 0
                    ? new Range(0, 0)
                    : new Range(sensor.Position.X - horizontalCoverage, sensor.Position.X + horizontalCoverage + 1);
            })
            .Where(range => range.Start != range.End)
            .OrderBy(range => range.Start)
            .ThenBy(range => range.End)
            .Normalize();

        var rangesList = ranges as List<Range> ?? ranges.ToList();
        var coveredPositions = rangesList.Select(x => x.End - x.Start).Sum();
        var beaconsInCoveredPositions = sensors
            .Select(x => x.DetectedBeacon.Position)
            .Concat(sensors.Select(sensor => sensor.Position))
            .Distinct()
            .Count(coordinate => coordinate.Y == _targetRow && rangesList.Any(range => range.Start <= coordinate.X && range.End > coordinate.X));
        return coveredPositions - beaconsInCoveredPositions;
    }
}
