namespace CodeChallenge.Core.Extensions;

public static class EnumerableExtensions
{
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

    public static void Deconstruct<T1, T2>(this IEnumerable<(T1, T2)> collection, out IEnumerable<T1> first, out IEnumerable<T2> second)
    {
        var queryable = collection.AsQueryable();
        first = queryable.Select(x => x.Item1);
        second = queryable.Select(x => x.Item2);
    }
}

[Flags]
public enum ChunkWhenFlags
{
    None                          = 0,
    IncludeMatchedItemInChunk     = 1 << 0,
    IncludeMatchedItemInNextChunk = 1 << 1
}