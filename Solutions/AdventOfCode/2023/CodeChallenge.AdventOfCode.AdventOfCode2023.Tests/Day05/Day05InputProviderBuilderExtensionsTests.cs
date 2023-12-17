namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day05;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day05;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day05.Models;
using CodeChallenge.Core.IO;

public class Day05InputProviderBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProviderBuilder<AdventOfCodeChallengeSelection> _inputProviderBuilder;

    public Day05InputProviderBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_ParsesAlmanacFromLinesOfText()
    {
        var input = new[]
        {
            "seeds: 79 14 55 13",
            "",
            "seed-to-soil map:",
            "50 98 2",
            "52 50 48",
            "",
            "soil-to-fertilizer map:",
            "0 15 37",
            "37 52 2",
            "39 0 15",
            "",
            "fertilizer-to-water map:",
            "49 53 8",
            "0 11 42",
            "42 0 7",
            "57 7 4",
            "",
            "water-to-light map:",
            "88 18 7",
            "18 25 70",
            "",
            "light-to-temperature map:",
            "45 77 23",
            "81 45 19",
            "68 64 13",
            "",
            "temperature-to-humidity map:",
            "0 69 1",
            "1 0 69",
            "",
            "humidity-to-location map:",
            "60 56 37",
            "56 93 4"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\r\n", input));

        var result = await _inputProviderBuilder.BuildDay05InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        var expectedSeedNumbers = new[] { 79L, 14L, 55L, 13L };

        var expectedSeedToSoilMap = new Map(new List<Mapping>
        {
            new(98, 50, 2),
            new(50, 52, 48)
        });

        var expectedSoilToFertilizerMap = new Map(new List<Mapping>
        {
            new(15, 0, 37),
            new(52, 37, 2),
            new(0, 39, 15)
        });

        var expectedFertilizerToWaterMap = new Map(new List<Mapping>
        {
            new(53, 49, 8),
            new(11, 0, 42),
            new(0, 42, 7),
            new(7, 57, 4)
        });

        var expectedWaterToLightMap = new Map(new List<Mapping>
        {
            new(18, 88, 7),
            new(25, 18, 70)
        });

        var expectedLightToTemperatureMap = new Map(new List<Mapping>
        {
            new(77, 45, 23),
            new(45, 81, 19),
            new(64, 68, 13)
        });

        var expectedTemperatureToHumidityMap = new Map(new List<Mapping>
        {
            new(69, 0, 1),
            new(0, 1, 69)
        });

        var expectedHumidityToLocationMap = new Map(new List<Mapping>
        {
            new(56, 60, 37),
            new(93, 56, 4)
        });

        Assert.Equal(expectedSeedNumbers, result.SeedIds);
        Assert.Collection(result.Maps,
            seedToSoilMap => Assert.Equal(expectedSeedToSoilMap.Mappings, seedToSoilMap.Mappings),
            soilToFertilizerMap => Assert.Equal(expectedSoilToFertilizerMap.Mappings, soilToFertilizerMap.Mappings),
            fertilizerToWaterMap => Assert.Equal(expectedFertilizerToWaterMap.Mappings, fertilizerToWaterMap.Mappings),
            waterToLightMap => Assert.Equal(expectedWaterToLightMap.Mappings, waterToLightMap.Mappings),
            lightToTemperatureMap => Assert.Equal(expectedLightToTemperatureMap.Mappings, lightToTemperatureMap.Mappings),
            temperatureToHumidityMap => Assert.Equal(expectedTemperatureToHumidityMap.Mappings, temperatureToHumidityMap.Mappings),
            humidityToLocationMap => Assert.Equal(expectedHumidityToLocationMap.Mappings, humidityToLocationMap.Mappings)
        );
    }
}