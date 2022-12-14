namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Tests.Day04;

using CodeChallenge.AdventOfCode.AdventOfCode2022.Day04;
using CodeChallenge.Core.IO;

using Range = System.Range;

public class Day04InputProviderBuilderExtensionsTests
{
    private readonly Mock<IInputReader<AdventOfCodeChallengeSelection>> _inputReaderMock;
    private readonly IInputProvider<AdventOfCodeChallengeSelection, IEnumerable<(Range, Range)>> _inputProvider;

    public Day04InputProviderBuilderExtensionsTests()
    {
        _inputReaderMock = new Mock<IInputReader<AdventOfCodeChallengeSelection>>();
        _inputProvider = new InputProviderBuilder<AdventOfCodeChallengeSelection>(_inputReaderMock.Object)
            .BuildDay04InputProvider();
    }

    [Theory]
    [MemberData(nameof(GetSampleInput))]
    public async Task ParseInputAsync_GivenSampleInput_ProducesSampleOutput((Range, Range) expected, string input)
    {
        // Arrange
        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<AdventOfCodeChallengeSelection>()))
            .ReturnsAsync(() => input);

        // Act
        var result = (await _inputProvider.GetInputAsync(new AdventOfCodeChallengeSelection(0, 0, 0)).ConfigureAwait(false)).Single();

        // Assert
        Assert.Equal(expected, result);
    }

    public static IEnumerable<object[]> GetSampleInput()
    {
        yield return new object[] { (new Range(2, 5), new Range(6, 9)), "2-4,6-8" };
        yield return new object[] { (new Range(2, 4), new Range(4, 6)), "2-3,4-5" };
        yield return new object[] { (new Range(5, 8), new Range(7, 10)), "5-7,7-9" };
        yield return new object[] { (new Range(2, 9), new Range(3, 8)), "2-8,3-7" };
        yield return new object[] { (new Range(6, 7), new Range(4, 7)), "6-6,4-6" };
        yield return new object[] { (new Range(2, 7), new Range(4, 9)), "2-6,4-8" };
    }
}