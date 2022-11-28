namespace CodeChallenge.AdventOfCode.Configuration;

using CodeChallenge.Core.Configuration;

public class AdventOfCodeConfiguration : ICodeChallengeConfiguration
{
    public bool DownloadMissingInput { get; set; }

    public string? Session { get; set; }
}