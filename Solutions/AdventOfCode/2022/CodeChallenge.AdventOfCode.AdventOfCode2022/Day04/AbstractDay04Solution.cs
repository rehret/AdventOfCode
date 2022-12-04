namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day04;

using CodeChallenge.Core.IO;

internal abstract class AbstractDay04Solution : AdventOfCodeSolution<IEnumerable<string>, int>
{
    protected AbstractDay04Solution(IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<string>> inputProvider) : base(inputProvider) { }

    protected static IEnumerable<(Range, Range)> BuildRangePairs(IEnumerable<string> input) =>
        input
            .Select(x => x.Split(',', 2, StringSplitOptions.TrimEntries))
            .Select(x => (BuildRange(x[0]), BuildRange(x[1])));

    private static Range BuildRange(string rangeString)
    {
        var parts = rangeString.Split('-', 2, StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToArray();

        // Add one to the second value because the input uses an inclusive upper bound and Range uses an exclusive upper bound
        return new Range(parts[0], parts[1] + 1);
    }
}