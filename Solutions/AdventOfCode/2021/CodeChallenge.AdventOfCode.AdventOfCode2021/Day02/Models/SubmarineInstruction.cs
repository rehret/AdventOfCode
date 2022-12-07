namespace CodeChallenge.AdventOfCode.AdventOfCode2021.Day02.Models;

public record SubmarineInstruction(SubmarineMovement Movement, int Amount)
    : IParsable<SubmarineInstruction>
{
    private static readonly SubmarineInstruction Default = new SubmarineInstruction(SubmarineMovement.Forward, 0);

    public static SubmarineInstruction Parse(string s, IFormatProvider? provider)
    {
        if (!TryParse(s, provider, out var instruction))
        {
            throw new FormatException($"Could not parse {nameof(SubmarineInstruction)} from '{s}'");
        }

        return instruction;
    }

    public static bool TryParse(string? s, IFormatProvider? provider, out SubmarineInstruction result)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            result = Default;
            return false;
        }

        var values = s.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (values.Length > 2)
        {
            result = Default;
            return false;
        }

        var movement = GetMovementFromString(values[0]);
        if (movement == null)
        {
            result = Default;
            return false;
        }

        if (!int.TryParse(values[1], out var amount))
        {
            result = Default;
            return false;
        }

        result = new SubmarineInstruction(movement.Value, amount);
        return true;
    }

    private static SubmarineMovement? GetMovementFromString(string movement) => movement.ToLower() switch
    {
        "forward" => SubmarineMovement.Forward,
        "up"      => SubmarineMovement.Up,
        "down"    => SubmarineMovement.Down,
        _         => null
    };
}