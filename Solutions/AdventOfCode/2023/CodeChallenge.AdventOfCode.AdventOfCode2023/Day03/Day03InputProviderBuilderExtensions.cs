namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day03;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day03.Models;
using CodeChallenge.Core.IO;

internal static class Day03InputProviderBuilderExtensions
{
    public static IInputProvider<AdventOfCodeChallengeSelection, Schematic> BuildDay03InputProvider(
        this IInputProviderBuilder<AdventOfCodeChallengeSelection> builder
    )
    {
        return builder
            .ReadAllInput(true)
            .ParseUsing(input =>
            {
                var parts = new List<Part>();
                var danglingLabels = new List<int>();

                var lines = input.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                for (var lineNumber = 0; lineNumber < lines.Length; lineNumber++)
                {
                    for (var charNumber = 0; charNumber < lines[lineNumber].Length; charNumber++)
                    {
                        var character = lines[lineNumber][charNumber];
                        if (char.IsDigit(character))
                        {
                            var labelLength = GetLabelLength(lines[lineNumber], charNumber);
                            var label = lines[lineNumber].Substring(charNumber, labelLength);
                            if (TryGetSymbolForLabel(lines, (X: charNumber, Y: lineNumber), labelLength, out var symbol, out var symbolCoordinates))
                            {
                                parts.Add(new Part(int.Parse(label), symbol, symbolCoordinates));
                            }
                            else
                            {
                                danglingLabels.Add(int.Parse(label));
                            }

                            charNumber += labelLength - 1; // We subtract 1 here because charNumber will be incremented by 1 at the end of the iteration
                        }
                    }
                }

                return new Schematic(parts, danglingLabels);
            })
            .Build();
    }

    private static int GetLabelLength(string line, int startPosition)
    {
        var length = 1;
        for (var i = startPosition + 1; i < line.Length && char.IsDigit(line[i]); i++)
        {
            length++;
        }

        return length;
    }

    private static bool TryGetSymbolForLabel(
        IReadOnlyList<string> lines,
        (int X, int Y) labelStart,
        int labelLength,
        out char symbol,
        out (int X, int Y) symbolCoordinates
    )
    {
        for (var searchY = Math.Max(labelStart.Y - 1, 0); searchY < Math.Min(labelStart.Y + 2, lines.Count); searchY++)
        {
            for (var searchX = Math.Max(labelStart.X - 1, 0); searchX < Math.Min(labelStart.X + labelLength + 1, lines[labelStart.Y].Length); searchX++)
            {
                var character = lines[searchY][searchX];
                if (character != IgnoredCharacter && !char.IsDigit(character))
                {
                    symbol = character;
                    symbolCoordinates = (X: searchX, Y: searchY);
                    return true;
                }
            }
        }

        symbol = IgnoredCharacter;
        symbolCoordinates = (X: -1, Y: -1);
        return false;
    }

    private const char IgnoredCharacter = '.';
}