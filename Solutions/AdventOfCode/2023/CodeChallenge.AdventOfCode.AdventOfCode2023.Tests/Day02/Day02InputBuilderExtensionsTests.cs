namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Tests.Day02;

using CodeChallenge.AdventOfCode.AdventOfCode2023.Day02;
using CodeChallenge.AdventOfCode.AdventOfCode2023.Day02.Models;
using CodeChallenge.Core.IO;

public class Day02InputBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProviderBuilder<AdventOfCodeChallengeSelection> _inputProviderBuilder;

    public Day02InputBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProviderBuilder = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object);
    }

    [Fact]
    public async Task GetInputAsync_GivenSampleInput_ParsesGamesWithDiceSetsFromLinesOfText()
    {
        var input = new[]
        {
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        };
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(string.Join("\n", input));

        var result = await _inputProviderBuilder.BuildDay02InputProvider()
            .GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0))
            .ConfigureAwait(false);

        Assert.Collection(result,
            game =>
            {
                Assert.Equal(1u, game.GameId);
                Assert.Collection(game.DiceSets,
                    set => Assert.Equal(new DiceSet(4, 0, 3), set),
                    set => Assert.Equal(new DiceSet(1, 2, 6), set),
                    set => Assert.Equal(new DiceSet(0, 2, 0), set)
                );
            },
            game =>
            {
                Assert.Equal(2u, game.GameId);
                Assert.Collection(game.DiceSets,
                    set => Assert.Equal(new DiceSet(0, 2, 1), set),
                    set => Assert.Equal(new DiceSet(1, 3, 4), set),
                    set => Assert.Equal(new DiceSet(0, 1, 1), set)
                );
            },
            game =>
            {
                Assert.Equal(3u, game.GameId);
                Assert.Collection(game.DiceSets,
                    set => Assert.Equal(new DiceSet(20, 8, 6), set),
                    set => Assert.Equal(new DiceSet(4, 13, 5), set),
                    set => Assert.Equal(new DiceSet(1, 5, 0), set)
                );
            },
            game =>
            {
                Assert.Equal(4u, game.GameId);
                Assert.Collection(game.DiceSets,
                    set => Assert.Equal(new DiceSet(3, 1, 6), set),
                    set => Assert.Equal(new DiceSet(6, 3, 0), set),
                    set => Assert.Equal(new DiceSet(14, 3, 15), set)
                );
            },
            game =>
            {
                Assert.Equal(5u, game.GameId);
                Assert.Collection(game.DiceSets,
                    set => Assert.Equal(new DiceSet(6, 3, 1), set),
                    set => Assert.Equal(new DiceSet(1, 2, 2), set)
                );
            }
        );
    }
}