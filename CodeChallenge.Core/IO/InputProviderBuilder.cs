namespace CodeChallenge.Core.IO;

using CodeChallenge.Core.Extensions;
using CodeChallenge.Core.IO.InputProviderBuilder;

public class InputProviderBuilder<TChallengeSelection>
    : IInputProviderBuilder<TChallengeSelection>
    where TChallengeSelection : ChallengeSelection
{
    private readonly IInputReader<TChallengeSelection> _inputReader;

    public InputProviderBuilder(IInputReader<TChallengeSelection> inputReader)
    {
        _inputReader = inputReader;
    }

    public IStringInputBuilder<TChallengeSelection> ReadAllInput(bool trim = false)
    {
        return new StringInputBuilder<TChallengeSelection>(async challengeSelection =>
        {
            var input = await _inputReader.GetInputAsync(challengeSelection).ConfigureAwait(false);

            if (trim)
            {
                input = input.Trim();
            }

            return input;
        });
    }

    public ILinesInputBuilder<TChallengeSelection> ReadLines(StringSplitOptions splitOptions = StringSplitOptions.None)
    {
        return new LinesInputBuilder<TChallengeSelection>(async challengeSelection =>
        {
            var fileContents = (await _inputReader.GetInputAsync(challengeSelection).ConfigureAwait(false));
            var lines = fileContents.Split('\n', splitOptions);
            return lines;
        });
    }

    public IChunkedInputBuilder<TChallengeSelection> ReadChunks(Func<string, bool> chunkSelector, ChunkWhenFlags flags = ChunkWhenFlags.None, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
    {
        return new ChunkedInputBuilder<TChallengeSelection>(async challengeSelection =>
            (await _inputReader.GetInputAsync(challengeSelection).ConfigureAwait(false))
            .Split('\n', stringSplitOptions)
            .ChunkWhen(chunkSelector, flags));
    }

    public IChunkedInputBuilder<TChallengeSelection> ReadChunks(Func<string, int, bool> chunkSelector, ChunkWhenFlags flags = ChunkWhenFlags.None, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
    {
        return new ChunkedInputBuilder<TChallengeSelection>(async challengeSelection =>
            (await _inputReader.GetInputAsync(challengeSelection).ConfigureAwait(false))
            .Split('\n', stringSplitOptions)
            .ChunkWhen(chunkSelector, flags));
    }
}