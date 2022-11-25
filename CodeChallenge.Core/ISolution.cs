namespace CodeChallenge.Core;

using System.Diagnostics;

public interface ISolution
{
    Task<string> SolveAsync(Stopwatch? stopwatch = null);
}