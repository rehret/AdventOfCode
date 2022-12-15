namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day15;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day15.Models;
using CodeChallenge.AdventOfCode.Attributes;
using CodeChallenge.Core.IO;

[AdventOfCodeSolution(2022, 15, 2)]
internal class Solution02 : AdventOfCodeSolution<IEnumerable<Sensor>, long>
{
    private const int ResultXMultiplier = 4000000;

    private readonly Coordinate _lowerBound;
    private readonly Coordinate _upperBound;

    public Solution02(
        IInputProviderBuilder<AdventOfCodeChallengeSelection> inputProviderBuilder,
        Coordinate? lowerBound = null,
        Coordinate? upperBound = null
    )
        : base(inputProviderBuilder.BuildDay15InputProvider())
    {
        _lowerBound = lowerBound ?? new Coordinate(0, 0);
        _upperBound = upperBound ?? new Coordinate(4000000, 4000000);
    }

    protected override long ComputeSolution(IEnumerable<Sensor> input)
    {
        var sensors = input.ToList();
        var coveredRangesInRows = Enumerable.Range(_lowerBound.Y, 1 + _upperBound.Y - _lowerBound.Y)
            .Select(y => GetCoveredRangesForRow(sensors, y))
            .Select(row =>
            {
                return row.Where(range => range.Start <= _upperBound.X || range.End >= _lowerBound.X)
                    .Select(range =>
                        new Range(Math.Max(_lowerBound.X, range.Start), Math.Min(_upperBound.X + 1, range.End)))
                    .Normalize();
            })
            .ToList();

        var target = coveredRangesInRows
            .Select((row, i) => (Row: row, Y: i + _lowerBound.Y))
            .MinBy(tuple => tuple.Row.Sum(range => range.End - range.Start));

        for (var x = _lowerBound.X; x <= _upperBound.X; x++)
        {
            if (!target.Row.Any(range => range.Contains(x)))
            {
                return (long)x * ResultXMultiplier + target.Y;
            }
        }

        return -1;
    }

    private static IEnumerable<Range> GetCoveredRangesForRow(IEnumerable<Sensor> sensors, int y)
    {
        return sensors
            .Select(sensor =>
            {
                var distanceToBeacon = sensor.Position.GetDistanceTo(sensor.DetectedBeacon.Position);
                var distanceToTargetRow = sensor.Position.GetDistanceTo(sensor.Position with { Y = y });
                var horizontalCoverage = distanceToBeacon - distanceToTargetRow;

                return horizontalCoverage <= 0
                    ? new Range(0, 0)
                    : new Range(sensor.Position.X - horizontalCoverage,
                        sensor.Position.X + horizontalCoverage + 1);
            })
            .Where(range => range.Start != range.End)
            .OrderBy(range => range.Start)
            .ThenBy(range => range.End)
            .Normalize();
    }
}
