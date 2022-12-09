namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day09.Models;

internal record Coordinate(int X, int Y)
{
    public Coordinate Move(MoveDirection direction) => direction switch
    {
        MoveDirection.Up        => this with { Y = Y + 1 },
        MoveDirection.Left      => this with { X = X - 1 },
        MoveDirection.Down      => this with { Y = Y - 1 },
        MoveDirection.Right     => this with { X = X + 1 },
        MoveDirection.UpLeft    => new Coordinate(X: X - 1, Y: Y + 1),
        MoveDirection.DownLeft  => new Coordinate(X: X - 1, Y: Y - 1),
        MoveDirection.DownRight => new Coordinate(X: X + 1, Y: Y - 1),
        MoveDirection.UpRight   => new Coordinate(X: X + 1, Y: Y + 1),
        _                       => throw new ArgumentOutOfRangeException(nameof(direction))
    };

    public Coordinate MoveTowards(Coordinate other)
    {
        if (other.X == X)
        {
            return this with { Y = Y + Math.Sign(other.Y - Y) };
        }

        if (other.Y == Y)
        {
            return this with { X = X + Math.Sign(other.X - X) };
        }

        if (other.X > X)
        {
            return other.Y > Y
                ? new Coordinate(X: X + 1, Y: Y + 1)
                : new Coordinate(X: X + 1, Y: Y - 1);
        }

        return other.Y > Y
            ? new Coordinate(X: X - 1, Y: Y + 1)
            : new Coordinate(X: X - 1, Y: Y - 1);
    }

    /// <summary>
    /// Computes distance between two Coordinates along cardinal directions and diagonals
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int DistanceTo(Coordinate other)
    {
        var xDistance = Math.Abs(other.X - X);
        var yDistance = Math.Abs(other.Y - Y);

        var smallestDistance = xDistance < yDistance ? xDistance : yDistance;
        var largestDistance = xDistance > yDistance ? xDistance : yDistance;

        // For smallestDistance number of steps, Coordinate can be moved diagonally
        // After that, the remaining distance must be covered in a cardinal direction
        return smallestDistance + (largestDistance - smallestDistance);
    }
}