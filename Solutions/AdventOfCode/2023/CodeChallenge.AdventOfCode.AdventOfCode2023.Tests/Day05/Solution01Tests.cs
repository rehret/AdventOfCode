namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day05;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day05;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day05.Models;
using CodeChallenge.Core.IO;

public class Solution01Tests
{
    private readonly Solution01 _solution;

    public Solution01Tests()
    {
        var inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        var inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(inputReaderMock.Object);
        _solution = new Solution01(inputProviderBuilder);
    }

    [Fact]
    public async Task ComputeSolutionAsync_GivenSampleInput_ProducesSampleOutput()
    {
        var seedNumbers = new[] { 79L, 14L, 55L, 13L };

        var seedToSoilMap = new Map(new List<Mapping>
        {
            new(98, 50, 2),
            new(50, 52, 48)
        });

        var soilToFertilizerMap = new Map(new List<Mapping>
        {
            new(15, 0, 37),
            new(52, 37, 2),
            new(0, 39, 15)
        });

        var fertilizerToWaterMap = new Map(new List<Mapping>
        {
            new(53, 49, 8),
            new(11, 0, 42),
            new(0, 42, 7),
            new(7, 57, 4)
        });

        var waterToLightMap = new Map(new List<Mapping>
        {
            new(18, 88, 7),
            new(25, 18, 70)
        });

        var lightToTemperatureMap = new Map(new List<Mapping>
        {
            new(77, 45, 23),
            new(45, 81, 19),
            new(64, 68, 13)
        });

        var temperatureToHumidityMap = new Map(new List<Mapping>
        {
            new(69, 0, 1),
            new(0, 1, 69)
        });

        var humidityToLocationMap = new Map(new List<Mapping>
        {
            new(56, 60, 37),
            new(93, 56, 4)
        });

        var maps = new[]
        {
            seedToSoilMap,
            soilToFertilizerMap,
            fertilizerToWaterMap,
            waterToLightMap,
            lightToTemperatureMap,
            temperatureToHumidityMap,
            humidityToLocationMap
        };

        var input = new Almanac(seedNumbers, maps);

        var result = await _solution.ComputeSolutionAsync(input).ConfigureAwait(false);

        Assert.Equal(35, result);
    }
}