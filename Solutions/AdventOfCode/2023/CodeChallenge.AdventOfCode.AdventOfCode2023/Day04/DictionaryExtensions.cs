namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day04;

internal static class DictionaryExtensions
{
    public static TValue GetOrSet<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary,
        TKey key,
        Func<TValue> valueProvider
    )
    {
        if (dictionary.TryGetValue(key, out var value))
        {
            return value;
        }

        value = valueProvider();
        dictionary[key] = value;
        return value;
    }
}