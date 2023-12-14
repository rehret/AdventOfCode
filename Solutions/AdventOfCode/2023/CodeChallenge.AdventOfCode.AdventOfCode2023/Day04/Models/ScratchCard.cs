namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day04.Models;

internal record ScratchCard(int CardNumber, IEnumerable<int> WinningNumbers, IEnumerable<int> ScratchedNumbers);
