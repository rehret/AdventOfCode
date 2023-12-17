namespace CodeChallenge.AdventOfCode.AdventOfCode2023.Day05.Models;

internal record Map(IEnumerable<Mapping> Mappings)
{
    public long MapValue(long value)
    {
        // We get the _last_ valid mapping in case mappings defined later are supposed to overwrite
        // previous mapping definitions
        var mapping = Mappings.LastOrDefault(mapping => mapping.HasMappingForValue(value));
        return mapping?.MapValue(value) ?? value;
    }
}