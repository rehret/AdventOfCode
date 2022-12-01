namespace CodeChallenge.AdventOfCode.IO;

using System.Net;
using System.Net.Http.Headers;
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
        if (string.IsNullOrWhiteSpace(_configuration.UserAgentHeader))
            throw new ArgumentNullException(nameof(_configuration.UserAgentHeader), "User-Agent header is required for fetching input. Should be in the format 'github.com/<user>/<repo> by user@email.com'");

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
        var remoteFilePath = AdventOfCodeResourcePathBuilder.GetRemoteFilePath(challengeSelection);
        using var httpClientHandler = new HttpClientHandler { CookieContainer = new CookieContainer() };
        httpClientHandler.CookieContainer.Add(new Cookie("session", _configuration.Session, string.Empty, remoteFilePath.Host));
        using var httpClient = new HttpClient(httpClientHandler);

        var request = new HttpRequestMessage(HttpMethod.Get, remoteFilePath);
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue(".NET", $"{Environment.Version.Major}.{Environment.Version.Minor}"));
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue("CodeChallenges", "1.0"));
        request.Headers.UserAgent.Add(new ProductInfoHeaderValue($"(+{_configuration.UserAgentHeader})"));

        var httpResponse = await httpClient.SendAsync(request).ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();

        return await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
    }
}