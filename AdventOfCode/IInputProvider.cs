namespace AdventOfCode;

public interface IInputProvider<T>
{
    Task<IEnumerable<T>> GetInputAsync(PuzzleSelection puzzleSelection);
}