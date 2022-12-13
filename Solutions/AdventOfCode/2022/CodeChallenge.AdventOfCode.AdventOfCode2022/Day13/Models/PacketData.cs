namespace CodeChallenge.AdventOfCode.AdventOfCode2022.Day13.Models;

internal abstract record PacketData : IComparable<PacketData?>
{
    public abstract int CompareTo(PacketData? other);
}

internal record ListPacketData(params PacketData[] Data) : PacketData
{
    public override int CompareTo(PacketData? other)
    {
        switch (other)
        {
            case null:
                return 1;
            case ListPacketData listPacketData:
            {
                for (var i = 0; i < Data.Length; i++)
                {
                    if (i == listPacketData.Data.Length)
                    {
                        return 1;
                    }

                    var comparisonResult = Data[i].CompareTo(listPacketData.Data[i]);
                    if (comparisonResult != 0)
                    {
                        return comparisonResult;
                    }
                }

                if (Data.Length < listPacketData.Data.Length)
                {
                    return -1;
                }

                return 0;
            }
            case IntegerPacketData integerPacketData:
            {
                var otherAsList = new ListPacketData(new PacketData[] { integerPacketData });
                return CompareTo(otherAsList);
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(other), $"Unexpected packet data type: '{other.GetType().Name}'");
        }
    }
}

internal record IntegerPacketData(int Value) : PacketData
{
    public override int CompareTo(PacketData? other)
    {
        switch (other)
        {
            case null:
                return 1;
            case ListPacketData listPacketData:
            {
                var thisAsList = new ListPacketData(new PacketData[] { this });
                return thisAsList.CompareTo(listPacketData);
            }
            case IntegerPacketData integerPacketData:
            {
                return Value.CompareTo(integerPacketData.Value);
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(other), $"Unexpected packet data type: '{other.GetType().Name}'");
        }
    }
}