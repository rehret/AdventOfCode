namespace CodeChallenge.Core.Tests.IO.InputProviders;

using CodeChallenge.Core.Extensions;
using CodeChallenge.Core.IO;
using CodeChallenge.Core.IO.InputProviders;

public class AbstractChunkedInputProviderTests
{
    private readonly Mock<IInputReader<ChallengeSelection>> _inputReaderMock;

    public AbstractChunkedInputProviderTests()
    {
        _inputReaderMock = new Mock<IInputReader<ChallengeSelection>>();
    }

    [Fact]
    public async Task GetInputAsync_WhenChunkingOnEmptyLines_CreatesAppropriateChunks()
    {
        // Arrange
        // We're using a ChunkedStringInputProvider here because it's essentially a no-op concrete implementation
        // of AbstractChunkedInputProvider
        var inputProvider = new ChunkedStringInputProvider<ChallengeSelection>(
            _inputReaderMock.Object,
            (line, _) => string.IsNullOrWhiteSpace(line),
            // ReSharper disable once RedundantArgumentDefaultValue We want to be explicit in our chosen flags and not rely on defaults
            ChunkWhenFlags.None
        );

        var input = new[]
        {
            "1",
            "2",
            "3",
            "", // Will chunk on this line
            "4",
            "5",
            "6"
        };

        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<ChallengeSelection>()))
            .ReturnsAsync(() => input);

        // Act
        var result = await inputProvider.GetInputAsync(new ChallengeSelection()).ConfigureAwait(false);

        // Assert
        Assert.Collection(
            result,
            firstChunk => Assert.Collection(
                firstChunk,
                x => Assert.Equal("1", x),
                x => Assert.Equal("2", x),
                x => Assert.Equal("3", x)
            ),
            secondChunk => Assert.Collection(
                secondChunk,
                x => Assert.Equal("4", x),
                x => Assert.Equal("5", x),
                x => Assert.Equal("6", x)
            )
        );
    }

    [Fact]
    public async Task GetInputAsync_WhenUsingNonDefaultChunkSelector_CreatesAppropriateChunks()
    {
        // Arrange
        // We're using a ChunkedStringInputProvider here because it's essentially a no-op concrete implementation
        // of AbstractChunkedInputProvider
        var inputProvider = new ChunkedStringInputProvider<ChallengeSelection>(
            _inputReaderMock.Object,
            (line, _) => line.Length == 3,
            // ReSharper disable once RedundantArgumentDefaultValue We want to be explicit in our chosen flags and not rely on defaults
            ChunkWhenFlags.None
        );

        var input = new[]
        {
            "1",
            "12",
            "123", // Will chunk on this line
            "4",
            "45",
            "456" // Will chunk on this line
        };

        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<ChallengeSelection>()))
            .ReturnsAsync(() => input);

        // Act
        var result = await inputProvider.GetInputAsync(new ChallengeSelection()).ConfigureAwait(false);

        // Assert
        Assert.Collection(
            result,
            firstChunk => Assert.Collection(
                firstChunk,
                x => Assert.Equal("1", x),
                x => Assert.Equal("12", x)
            ),
            secondChunk => Assert.Collection(
                secondChunk,
                x => Assert.Equal("4", x),
                x => Assert.Equal("45", x)
            )
        );
    }

    [Fact]
    public async Task GetInputAsync_WhenFlagsIndicateToIncludeMatchedLineInChunk_ThenLineIsIncludedInChunk()
    {
        // Arrange
        // We're using a ChunkedStringInputProvider here because it's essentially a no-op concrete implementation
        // of AbstractChunkedInputProvider
        var inputProvider = new ChunkedStringInputProvider<ChallengeSelection>(
            _inputReaderMock.Object,
            (line, _) => line.Length == 3,
            ChunkWhenFlags.IncludeMatchedItemInChunk
        );

        var input = new[]
        {
            "1",
            "12",
            "123", // Will chunk on this line
            "4",
            "45",
            "456" // Will chunk on this line
        };

        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<ChallengeSelection>()))
            .ReturnsAsync(() => input);

        // Act
        var result = await inputProvider.GetInputAsync(new ChallengeSelection()).ConfigureAwait(false);

        // Assert
        Assert.Collection(
            result,
            firstChunk => Assert.Collection(
                firstChunk,
                x => Assert.Equal("1", x),
                x => Assert.Equal("12", x),
                x => Assert.Equal("123", x)
            ),
            secondChunk => Assert.Collection(
                secondChunk,
                x => Assert.Equal("4", x),
                x => Assert.Equal("45", x),
                x => Assert.Equal("456", x)
            )
        );
    }

    [Fact]
    public async Task GetInputAsync_WhenFlagsIndicateToIncludeMatchedLineInNextChunk_ThenLineIsIncludedInNextChunk()
    {
        // Arrange
        // We're using a ChunkedStringInputProvider here because it's essentially a no-op concrete implementation
        // of AbstractChunkedInputProvider
        var inputProvider = new ChunkedStringInputProvider<ChallengeSelection>(
            _inputReaderMock.Object,
            (line, _) => line.Length == 3,
            ChunkWhenFlags.IncludeMatchedItemInNextChunk
        );

        var input = new[]
        {
            "1",
            "12",
            "123", // Will chunk on this line
            "4",
            "45",
            "456" // Will chunk on this line
        };

        _inputReaderMock.Setup(x => x.GetInputAsync(It.IsAny<ChallengeSelection>()))
            .ReturnsAsync(() => input);

        // Act
        var result = await inputProvider.GetInputAsync(new ChallengeSelection()).ConfigureAwait(false);

        // Assert
        Assert.Collection(
            result,
            firstChunk => Assert.Collection(
                firstChunk,
                x => Assert.Equal("1", x),
                x => Assert.Equal("12", x)
            ),
            secondChunk => Assert.Collection(
                secondChunk,
                x => Assert.Equal("123", x),
                x => Assert.Equal("4", x),
                x => Assert.Equal("45", x)
            ),
            thirdChunk => Assert.Collection(
                thirdChunk,
                x => Assert.Equal("456", x)
            )
        );
    }
}