namespace AdventOfCode;

using System.Text.RegularExpressions;

public record PuzzleSelection(int Year, int Day, int Puzzle)
{
    private static readonly Regex ArgumentRegex = new(@"^(?<year>\d{4})/(?<day>\d{1,2})/(?<puzzle>\d{1,2}$)", RegexOptions.Compiled);

    public static PuzzleSelection FromArguments(string[] args)
    {
        if (args.Length == 0 || !args.Any(arg => ArgumentRegex.IsMatch(arg)))
        {
            throw new InvalidPuzzleSelectionException("No arguments matched the pattern <year>/<day>/<puzzle>");
        }

        var match = args.Select(arg => ArgumentRegex.Match(arg)).First(match => match.Success);

        if (!int.TryParse(match.Groups["year"].Value, out var year))
        {
            throw new PuzzleSelectionParseException($"Could not parse year: '{match.Groups["year"].Value}'");
        }

        if (!int.TryParse(match.Groups["day"].Value, out var day))
        {
            throw new PuzzleSelectionParseException($"Could not parse day: '{match.Groups["day"].Value}'");
        }

        if (!int.TryParse(match.Groups["puzzle"].Value, out var puzzle))
        {
            throw new PuzzleSelectionParseException($"Could not parse puzzle: '{match.Groups["puzzle"].Value}'");
        }

        return new PuzzleSelection(year, day, puzzle);
    }

    public class InvalidPuzzleSelectionException : Exception
    {
        public InvalidPuzzleSelectionException(string message) : base(message) { }
    }

    public class PuzzleSelectionParseException : Exception
    {
        public PuzzleSelectionParseException(string message) : base(message) { }
    }
}