namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day07.Models;

internal record Directory(string Name)
    : FileSystemEntity(Name), IParsable<Directory>
{
    public IList<FileSystemEntity> Entities { get; } = new List<FileSystemEntity>();

    public int GetSize()
    {
        return Entities.Aggregate(0, (sum, entity) => sum + entity switch
        {
            File file           => file.Size,
            Directory directory => directory.GetSize(),
            _                   => throw new ArgumentOutOfRangeException()
        });
    }

    public static Directory Parse(string s, IFormatProvider? provider)
    {
        if (!TryParse(s, provider, out var directory))
        {
            throw new FormatException($"Could not parse {nameof(Directory)} from '{s}'");
        }

        return directory;
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out Directory result)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            result = new Directory(string.Empty);
            return false;
        }

        var parts = s.Split(' ', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
        {
            result = new Directory(string.Empty);
            return false;
        }

        if (!string.Equals("dir", parts[0].ToLower()))
        {
            result = new Directory(string.Empty);
            return false;
        }

        result = new Directory(parts[1]);
        return true;
    }
}