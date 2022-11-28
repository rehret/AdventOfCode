namespace CodeChallenge.Core.Console;

using System.CommandLine;

public interface ICommandBuilder
{
    Command Build();
}