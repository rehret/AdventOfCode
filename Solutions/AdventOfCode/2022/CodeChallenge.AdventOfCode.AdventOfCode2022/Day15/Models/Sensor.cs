namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day15.Models;

using System.Text.RegularExpressions;

internal partial record Sensor(Coordinate Position, Beacon DetectedBeacon)
    : IParsable<Sensor>
{
    public static Sensor Parse(string s, IFormatProvider? provider)
    {
        if (!TryParse(s, provider, out var result))
        {
            throw new FormatException($"Could not parse {nameof(Sensor)} from '{s}'");
        }

        return result;
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Sensor result)
    {
        var @default = new Sensor(new Coordinate(0, 0), new Beacon(new Coordinate(0, 0)));
        if (string.IsNullOrWhiteSpace(s))
        {
            result = @default;
            return false;
        }

        var match = GetInputLineRegex().Match(s);
        if (!match.Success)
        {
            result = @default;
            return false;
        }

        if (!int.TryParse(match.Groups["SensorX"].Value, out var sensorX)
            || !int.TryParse(match.Groups["SensorY"].Value, out var sensorY)
            || !int.TryParse(match.Groups["BeaconX"].Value, out var beaconX)
            || !int.TryParse(match.Groups["BeaconY"].Value, out var beaconY))
        {
            result = @default;
            return false;
        }

        result = new Sensor(new Coordinate(sensorX, sensorY), new Beacon(new Coordinate(beaconX, beaconY)));
        return true;
    }

    [GeneratedRegex(@"^Sensor at x=(?<SensorX>-?\d+), y=(?<SensorY>-?\d+): closest beacon is at x=(?<BeaconX>-?\d+), y=(?<BeaconY>-?\d+)$")]
    private static partial Regex GetInputLineRegex();
}