﻿namespace CodeChallenge.Core.Extensions;

public static class EnumerableExtensions
{
    [Flags]
    public enum ChunkWhenFlags
    {
        IncludeMatchedItemInChunk     = 1 << 0,
        IncludeMatchedItemInNextChunk = 1 << 1
    }

    public static IEnumerable<IEnumerable<T>> ChunkWhen<T>(
        this IEnumerable<T> collection,
        Func<T, bool> selector,
        ChunkWhenFlags flags = 0x00
    ) => ChunkWhen(collection, (item, _) => selector(item), flags);

    public static IEnumerable<IEnumerable<T>> ChunkWhen<T>(
        this IEnumerable<T> collection,
        Func<T, int, bool> selector,
        ChunkWhenFlags flags = 0x00
    )
    {
        using var enumerator = collection.GetEnumerator();

        var chunk = Enumerable.Empty<T>();
        var chunkHasItems = false;
        var index = 0;
        while (enumerator.MoveNext())
        {
            if (selector(enumerator.Current, index))
            {
                yield return flags.HasFlag(ChunkWhenFlags.IncludeMatchedItemInChunk)
                    ? chunk.Append(enumerator.Current)
                    : chunk;

                chunk = Enumerable.Empty<T>();
                chunkHasItems = false;
                if (flags.HasFlag(ChunkWhenFlags.IncludeMatchedItemInNextChunk))
                {
                    chunk = chunk.Append(enumerator.Current);
                    chunkHasItems = true;
                }
            }
            else
            {
                chunk = chunk.Append(enumerator.Current);
                chunkHasItems = true;
            }

            index++;
        }

        if (chunkHasItems) yield return chunk;
    }
}