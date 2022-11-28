namespace CodeChallenge.AdventOfCode.IO;

internal interface IAdventOfCodeInputWriter
{
    Task FetchRemoteInputAsync(AdventOfCodeChallengeSelection challengeSelection);
}