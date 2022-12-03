namespace CodeChallenge.Core.IO;

using System.Collections;

using CodeChallenge.Core.Extensions;

public delegate IInputProvider<TChallengeSelection, TOutput>
    ChunkedInputProviderFactory<in TChallengeSelection, TOutput>(
        Func<string, int, bool> chunkSelector,
        ChunkWhenFlags chunkWhenFlags = 0x00
    )
    where TChallengeSelection : ChallengeSelection
    where TOutput : IEnumerable<IEnumerable>;