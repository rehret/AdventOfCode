namespace CodeChallenge;

public interface IChallengeArgumentParser
{
    string DisplayName { get; }

    string[] Aliases { get; }

    string[] ArgumentPartNames { get; }

    string GetUsage();

    /// <summary>
    ///
    /// </summary>
    /// <param name="challengeSelectionArgument">The first portion of the selection argument (everything up to the first '/')</param>
    /// <returns></returns>
    bool CanBeParsed(string challengeSelectionArgument);

    /// <summary>
    ///
    /// </summary>
    /// <param name="remainingArguments">The argument portion of the challenge selection argument (everything after the first '/')</param>
    /// <param name="challengeSelection"></param>
    /// <returns></returns>
    bool TryParse(string remainingArguments, out ChallengeSelection challengeSelection);
}