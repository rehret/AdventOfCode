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
        if (string.IsNullOrWhiteSpace(_configuration.Session)) throw new ArgumentNullException(nameof(_configuration.Session), "Session token is required for fetching input");

        var filepath = AdventOfCodeResourcePathBuilder.GetInputFilePath(challengeSelection);
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
        var remoteFilePath = AdventOfCodeResourcePathBuilder.GetRemoteFilePath(challengeSelection);
        httpClientHandler.CookieContainer.Add(new Cookie("session", _configuration.Session, string.Empty, remoteFilePath.Host));

        var httpResponse = await httpClient.GetAsync(remoteFilePath).ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();

        return await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
    }
}