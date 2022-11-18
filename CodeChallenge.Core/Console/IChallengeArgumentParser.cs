namespace CodeChallenge.Core.Console;

public interface IChallengeArgumentParser
{
    string DisplayName { get; }

    string[] Aliases { get; }

    string[] ArgumentPartNames { get; }

    string GetUsage();

    /// <summary></summary>
    /// <param name="challengeSelectionArgument">The first portion of the selection argument (everything up to the first '/')</param>
    /// <returns></returns>
    /// <remarks>If the CLI argument was "advent/2021/01/01", <paramref name="challengeSelectionArgument"/> would be "advent"</remarks>
    bool CanBeParsed(string challengeSelectionArgument);

    /// <summary></summary>
    /// <param name="remainingArguments">The argument portion of the challenge selection argument (everything after the first '/')</param>
    /// <param name="challengeSelection"></param>
    /// <returns></returns>
    /// <remarks>If the CLI argument was "advent/2021/01/01", <paramref name="remainingArguments"/> would be "2021/01/01"</remarks>
    bool TryParse(string remainingArguments, out ChallengeSelection challengeSelection);
}