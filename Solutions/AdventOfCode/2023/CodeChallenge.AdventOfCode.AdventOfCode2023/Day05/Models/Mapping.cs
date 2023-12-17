namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day05.Models;

internal record Mapping(long StartingSource, long StartingDestination, long Range)
{
    public bool HasMappingForValue(long value)
    {
        return value >= StartingSource && value < StartingSource + Range;
    }

    public long MapValue(long value)
    {
        return StartingDestination + (value - StartingSource);
    }
}