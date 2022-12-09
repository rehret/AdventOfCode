namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day09.Models;

internal record MoveInstruction(MoveDirection Direction, int Amount) : IParsable<MoveInstruction>
{
    public static MoveInstruction Parse(string s, IFormatProvider? provider)
    {
        if (!TryParse(s, provider, out var moveInstruction))
        {
            throw new FormatException($"Could not parse {nameof(MoveInstruction)} from '{s}'");
        }

        return moveInstruction;
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out MoveInstruction result)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            result = new MoveInstruction(default, default);
            return false;
        }

        var parts = s.Split(' ', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
        {
            result = new MoveInstruction(default, default);
            return false;
        }

        var moveDirection = ParseMoveDirection(parts[0]);
        if (moveDirection == null)
        {
            result = new MoveInstruction(default, default);
            return false;
        }

        if (!int.TryParse(parts[1], out var amount))
        {
            result = new MoveInstruction(default, default);
            return false;
        }

        result = new MoveInstruction(moveDirection.Value, amount);
        return true;
    }

    private static MoveDirection? ParseMoveDirection(string? s) => s switch
    {
        "U" => MoveDirection.Up,
        "L" => MoveDirection.Left,
        "D" => MoveDirection.Down,
        "R" => MoveDirection.Right,
        _   => null
    };
}