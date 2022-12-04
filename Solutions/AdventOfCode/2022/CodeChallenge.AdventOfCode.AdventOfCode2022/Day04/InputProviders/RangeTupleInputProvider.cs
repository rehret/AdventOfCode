namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day04.InputProviders;

using CodeChallenge.Core.IO;
using CodeChallenge.Core.IO.InputProviders;

internal class RangeTupleInputProvider : AbstractInputProvider<AdventOfCodeChallengeSelection, IEnumerable<(Range, Range)>>
{
    public RangeTupleInputProvider(IInputReader<AdventOfCodeChallengeSelection> inputReader) : base(inputReader) { }

    protected override IEnumerable<(Range, Range)> ParseLines(IEnumerable<string> lines) =>
        lines
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