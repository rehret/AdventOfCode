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
        if (Path.Exists(filepath))
        {
            using var streamReader = new StreamReader(filepath, Encoding.UTF8);
            fileContent = await streamReader.ReadToEndAsync().ConfigureAwait(false);
        }
        else if (_configuration.DownloadMissingInput)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filepath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filepath)!);
            }
            fileContent = await GetInputFromRemoteServer(challengeSelection).ConfigureAwait(false);
            var streamWriter = new StreamWriter(filepath, Encoding.UTF8, new FileStreamOptions { Mode = FileMode.CreateNew, Access = FileAccess.Write });
            await using var _ = streamWriter.ConfigureAwait(false);
            await streamWriter.WriteAsync(fileContent).ConfigureAwait(false);
        }
        else
        {
            throw new FileNotFoundException("Could not find input file for Advent of Code puzzle", filepath);
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