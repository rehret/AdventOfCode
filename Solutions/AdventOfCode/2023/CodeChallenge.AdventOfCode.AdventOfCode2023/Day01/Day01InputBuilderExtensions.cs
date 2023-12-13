namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day01;

using CodeChallenge.Core.IO;

internal static class Day01InputBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<IEnumerable<int>>> BuildDay01Puzzle01InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseUsing(line => line.ToCharArray().Where(char.IsDigit).Select(x => int.Parse(x.ToString())))
            .Build();
    }

    public static IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<IEnumerable<int>>> BuildDay01Puzzle02InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadLines(StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ParseUsing(line =>
            {
                var numbersOnLine = new List<char>();

                for (var i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        numbersOnLine.Add(line[i]);
                    }
                    else
                    {
                        if (line.NumberIsWrittenAtIndex("one", i))
                            numbersOnLine.Add('1');
                        if (line.NumberIsWrittenAtIndex("two", i))
                            numbersOnLine.Add('2');
                        if (line.NumberIsWrittenAtIndex("three", i))
                            numbersOnLine.Add('3');
                        if (line.NumberIsWrittenAtIndex("four", i))
                            numbersOnLine.Add('4');
                        if (line.NumberIsWrittenAtIndex("five", i))
                            numbersOnLine.Add('5');
                        if (line.NumberIsWrittenAtIndex("six", i))
                            numbersOnLine.Add('6');
                        if (line.NumberIsWrittenAtIndex("seven", i))
                            numbersOnLine.Add('7');
                        if (line.NumberIsWrittenAtIndex("eight", i))
                            numbersOnLine.Add('8');
                        if (line.NumberIsWrittenAtIndex("nine", i))
                            numbersOnLine.Add('9');
                    }
                }

                return numbersOnLine.Select(x => x.ToString()).Select(int.Parse);
            })
            .Build();
    }

    private static bool NumberIsWrittenAtIndex(this string s, string writtenNumber, int index)
    {
        try
        {
            return string.Equals(writtenNumber, s.Substring(index, writtenNumber.Length), StringComparison.OrdinalIgnoreCase);
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }
}