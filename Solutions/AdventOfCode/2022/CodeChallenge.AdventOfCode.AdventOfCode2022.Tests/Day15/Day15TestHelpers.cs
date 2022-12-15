namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day15;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day15.Models;

internal static class Day15TestHelpers
{
    public static IEnumerable<Sensor> BuildSampleInput()
    {
        return new List<Sensor>
        {
            new(new Coordinate(2, 18), new Beacon(new Coordinate(-2, 15))),
            new(new Coordinate(9, 16), new Beacon(new Coordinate(10, 16))),
            new(new Coordinate(13, 2), new Beacon(new Coordinate(15, 3))),
            new(new Coordinate(12, 14), new Beacon(new Coordinate(10, 16))),
            new(new Coordinate(10, 20), new Beacon(new Coordinate(10, 16))),
            new(new Coordinate(14, 17), new Beacon(new Coordinate(10, 16))),
            new(new Coordinate(8, 7), new Beacon(new Coordinate(2, 10))),
            new(new Coordinate(2, 0), new Beacon(new Coordinate(2, 10))),
            new(new Coordinate(0, 11), new Beacon(new Coordinate(2, 10))),
            new(new Coordinate(20, 14), new Beacon(new Coordinate(25, 17))),
            new(new Coordinate(17, 20), new Beacon(new Coordinate(21, 22))),
            new(new Coordinate(16, 7), new Beacon(new Coordinate(15, 3))),
            new(new Coordinate(14, 3), new Beacon(new Coordinate(15, 3))),
            new(new Coordinate(20, 1), new Beacon(new Coordinate(15, 3)))
        };
    }
}