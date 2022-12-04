namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day04.Extensions;

internal static class RangeExtensions
{
    internal static bool Contains(this Range range, Index index)
    {
        return range.Start.Value <= index.Value && range.End.Value > index.Value;
    }

    internal static bool Contains(this Range thisRange, Range otherRange)
    {
        // Subtract 1 from otherRange's End value because we want to check that the inclusive upper bound is in thisRange
        return thisRange.Contains(otherRange.Start) && thisRange.Contains(otherRange.End.Value - 1);
    }

    internal static bool Overlaps(this Range thisRange, Range otherRange)
    {
        return thisRange.Contains(otherRange.Start) || otherRange.Contains(thisRange.Start);
    }
}