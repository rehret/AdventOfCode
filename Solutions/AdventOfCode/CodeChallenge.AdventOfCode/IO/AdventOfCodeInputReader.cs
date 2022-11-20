namespace CodeChallenge.AdventOfCode.IO;

using System.Net;
using System.Text;

using CodeChallenge.AdventOfCode.Configuration;
using CodeChallenge.Core.IO;

using Microsoft.Extensions.Options;

internal class AdventOfCodeInputReader : IInputReader<AdventOfCodeChallengeSelection>
{
    private readonly AdventOfCodeConfiguration _configuration;

    public AdventOfCodeInputReader(IOptions<AdventOfCodeConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    public async Task<IEnumerable<string>> GetInputAsync(AdventOfCodeChallengeSelection challengeSelection)
    {
        var filepath = GetInputFilePath(challengeSelection);
        string fileContent;
        try
        {
            using var streamReader = new StreamReader(filepath, Encoding.UTF8);
            fileContent = await streamReader.ReadToEndAsync().ConfigureAwait(false);
        }
        catch (Exception ex) when (ex is FileNotFoundException or DirectoryNotFoundException && !string.IsNullOrWhiteSpace(_configuration.Session))
        {
            fileContent = await GetInputFromRemoteServer(challengeSelection).ConfigureAwait(false);
        }
        return fileContent.Split('\n', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }

    private async Task<string> GetInputFromRemoteServer(AdventOfCodeChallengeSelection challengeSelection)
    {
        using var httpClientHandler = new HttpClientHandler { CookieContainer = new CookieContainer() };
        using var httpClient = new HttpClient(httpClientHandler);
        httpClientHandler.CookieContainer.Add(new Cookie("session", _configuration.Session, string.Empty, GetRemoteFilePath(challengeSelection).Host));

        var httpResponse = await httpClient.GetAsync(GetRemoteFilePath(challengeSelection)).ConfigureAwait(false);
        httpResponse.EnsureSuccessStatusCode();

        var fileStream = new FileStream(GetInputFilePath(challengeSelection), FileMode.CreateNew);
        await using var _ = fileStream.ConfigureAwait(false);
        await httpResponse.Content.CopyToAsync(fileStream).ConfigureAwait(false);
        fileStream.Close();

        return await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
    }

    private static string GetInputFilePath(AdventOfCodeChallengeSelection challengeSelection) =>
        Path.Combine(
            Environment.CurrentDirectory,
            $"Resources/AdventOfCode/{challengeSelection.Year:0000}/Day{challengeSelection.Day:00}.txt"
        );

    private static Uri GetRemoteFilePath(AdventOfCodeChallengeSelection challengeSelection) =>
        new($"https://adventofcode.com/{challengeSelection.Year:0000}/day/{challengeSelection.Day:0}/input");
}