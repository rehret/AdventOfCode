namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day15.Models;

// Using a custom Range type because System.Range doesn't support negative values
internal readonly record struct Range(int Start, int End)
{
    public bool Contains(int value)
    {
        return Start <= value && value < End;
    }
}

internal static class RangeIEnumerableExtensions
{
    public static IEnumerable<Range> Normalize(this IEnumerable<Range> ranges)
    {
        return ranges
            .Aggregate(new List<Range>(),
                (acc, range) =>
                {
                    if (!acc.Any())
                    {
                        acc.Add(range);
                        return acc;
                    }

                    if (range.Start >= acc.Last().Start && range.End <= acc.Last().End)
                    {
                        return acc;
                    }

                    if (range.Start < acc.Last().End)
                    {
                        if (range.End <= acc.Last().End)
                        {
                            return acc;
                        }

                        var last = acc.Last();
                        return acc.Take(acc.Count - 1)
                            .Append(new Range(Math.Min(last.Start, range.Start), Math.Max(last.End, range.End)))
                            .ToList();
                    }

                    acc.Add(range);
                    return acc;
                });
    }
}