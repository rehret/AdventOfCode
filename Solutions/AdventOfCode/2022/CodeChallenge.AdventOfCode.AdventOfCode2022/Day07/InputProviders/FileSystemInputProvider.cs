namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day07.InputProviders;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day07.Models;
using CodeChallenge.Core.Extensions;
using CodeChallenge.Core.IO;
using CodeChallenge.Core.IO.InputProviders;

internal class FileSystemInputProvider : AbstractInputProvider<AdventOfCodeChallengeSelection, Directory>
{
    public FileSystemInputProvider(IInputReader<AdventOfCodeChallengeSelection> inputReader) : base(inputReader) { }

    protected override Directory ParseLines(IEnumerable<string> lines)
    {
        var root = new Directory("/");
        var workingDirectory = new Stack<Directory>();
        workingDirectory.Push(root);

        var lineChunks = lines
            .ChunkWhen(line => line.StartsWith('$'), ChunkWhenFlags.IncludeMatchedItemInNextChunk)
            .Where(chunk => chunk.Any());

        foreach (var chunk in lineChunks)
        {
            var chunkLines = chunk as string[] ?? chunk.ToArray();

            var commandLine = chunkLines.First();
            if (!commandLine.StartsWith('$'))
            {
                throw new FormatException($"Received a command chunk that did not start with a command: '{commandLine}'");
            }

            var parts = commandLine.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var command = parts[1];
            var args = parts[2..];

            switch (command.ToLower())
            {
                case "cd":
                    ChangeDirectory(root, workingDirectory, args);
                    break;
                case "ls":
                    List(workingDirectory, chunkLines.Skip(1));
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unsupported command: '{string.Join(" ", args)}'");
            }
        }

        return root;
    }

    private static void ChangeDirectory(Directory root, Stack<Directory> workingDirectory, string[] args)
    {
        if (args.Length != 1)
        {
            throw new ArgumentException("Invalid number of arguments", nameof(args));
        }

        if (args[0] == "/")
        {
            workingDirectory = new Stack<Directory>();
            workingDirectory.Push(root);
            return;
        }

        if (args[0] == "..")
        {
            workingDirectory.Pop();
            return;
        }

        var directory = workingDirectory.Peek().Entities
            .OfType<Directory>()
            .SingleOrDefault(x => x.Name == args[0]);
        if (directory != null)
        {
            workingDirectory.Push(directory);
        }
        else
        {
            directory = new Directory(args[0]);
            workingDirectory.Peek().Entities.Add(directory);
            workingDirectory.Push(directory);
        }
    }

    private static void List(Stack<Directory> workingDirectory, IEnumerable<string> results)
    {
        foreach (var result in results)
        {
            var parts = result.Split(' ', 2, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                throw new FormatException($"Unexpected number of columns in 'ls' result: '{result}'");
            }

            FileSystemEntity entity = parts[0] switch
            {
                "dir" => new Directory(parts[1]),
                _ when int.TryParse(parts[0], out var size) => new File(parts[1], size),
                _ => throw new FormatException($"Unexpected format of 'ls' result: '{result}'")
            };

            workingDirectory.Peek().Entities.Add(entity);
        }
    }
}