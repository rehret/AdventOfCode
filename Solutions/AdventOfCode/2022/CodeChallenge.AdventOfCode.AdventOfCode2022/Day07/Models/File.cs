namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day07.Models;

internal record File(string Name, int Size)
    : FileSystemEntity(Name), IParsable<File>
{
    public static File Parse(string s, IFormatProvider? provider)
    {
        if (!TryParse(s, provider, out var file))
        {
            throw new FormatException($"Could not parse {nameof(File)} from '{s}'");
        }

        return file;
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out File result)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            result = new File(string.Empty, 0);
            return false;
        }

        var parts = s.Split(' ', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
        {
            result = new File(string.Empty, 0);
            return false;
        }

        if (!int.TryParse(parts[0], out var size))
        {
            result = new File(string.Empty, 0);
            return false;
        }

        result = new File(parts[1], size);
        return true;
    }
}