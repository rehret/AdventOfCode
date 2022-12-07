namespace CodeChallenge.Core.IO;

using CodeChallenge.Core.Extensions;
using CodeChallenge.Core.IO.InputProviderBuilder;

public interface IInputProviderBuilder<in TChallengeSelection>
    where TChallengeSelection : ChallengeSelection
{
    IStringInputBuilder<TChallengeSelection> ReadAllInput(bool trim = false);
    ILinesInputBuilder<TChallengeSelection> ReadLines(StringSplitOptions splitOptions = StringSplitOptions.None);
    IChunkedInputBuilder<TChallengeSelection> ReadChunks(Func<string, bool> chunkSelector, ChunkWhenFlags flags = ChunkWhenFlags.None, StringSplitOptions stringSplitOptions = StringSplitOptions.None);
    IChunkedInputBuilder<TChallengeSelection> ReadChunks(Func<string, int, bool> chunkSelector, ChunkWhenFlags flags = ChunkWhenFlags.None, StringSplitOptions stringSplitOptions = StringSplitOptions.None);
}