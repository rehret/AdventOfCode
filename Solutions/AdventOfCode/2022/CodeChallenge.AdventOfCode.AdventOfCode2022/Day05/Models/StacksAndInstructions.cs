namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day05.Models;

internal record StacksAndInstructions(Stack<char>[] Stacks, IEnumerable<MoveInstruction> Instructions);