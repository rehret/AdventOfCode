namespace CodeChallenge.AdventOfCode.IO;

using System.Net;
using System.Text;

using CodeChallenge.AdventOfCode.Configuration;

using Microsoft.Extensions.Options;

internal class AdventOfCodeInputWriter
    : IAdventOfCodeInputWriter
{
    private readonly AdventOfCodeConfiguration _configuration;

    public AdventOfCodeInputWriter(IOptions<AdventOfCodeConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public async Task FetchRemoteInputAsync(AdventOfCodeChallengeSelection challengeSelection)
    {
        var filepath = FilePathHelpers.GetInputFilePath(challengeSelection);
        if (!Directory.Exists(Path.GetDirectoryName(filepath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filepath)!);
        }
        var fileContent = await GetInputFromRemoteServer(challengeSelection).ConfigureAwait(false);
        var streamWriter = new StreamWriter(filepath, Encoding.UTF8, new FileStreamOptions { Mode = FileMode.CreateNew, Access = FileAccess.Write });
        await using var _ = streamWriter.ConfigureAwait(false);
        await streamWriter.WriteAsync(fileContent).ConfigureAwait(false);
    }

    private async Task<string> GetInputFromRemoteServer(AdventOfCodeChallengeSelection challengeSelection)
    {
        using var httpClientHandler = new HttpClientHandler { CookieContainer = new CookieContainer() };
        using var httpClient = new HttpClient(httpClientHandler);
        httpClientHandler.CookieContainer.Add(new Cookie("session", _configuration.Session, string.Empty, GetRemoteFilePath(challengeSelection).Host));

        var httpResponse = await httpClient.GetAsync(GetRemoteFilePath(challengeSelection)).ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();

        return await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
    }

    private static Uri GetRemoteFilePath(AdventOfCodeChallengeSelection challengeSelection) =>
        new($"https://adventofcode.com/{challengeSelection.Year:0000}/day/{challengeSelection.Day:0}/input");
}