namespace CodeChallenge.TomsDataOnion.Solutions.Layer4.Models;

public class InternetProtocolV4Address
    : IEquatable<InternetProtocolV4Address>, IEquatable<string>
{
    public byte[] Octets { get; }

    public InternetProtocolV4Address(byte[] octets)
    {
        if (octets.Length != 4)
        {
            throw new ArgumentException("Invalid number of octets in IPv4 address", nameof(octets));
        }

        Octets = octets;
    }

    public InternetProtocolV4Address(string ipAddress)
    {
        var parts = ipAddress.Split('.');
        if (parts.Length != 4)
        {
            throw new ArgumentException("Invalid number of octets in IPv4 address", nameof(ipAddress));
        }

        Octets = parts.Select(byte.Parse).ToArray();
    }

    public bool Equals(InternetProtocolV4Address? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        for (var i = 0; i < 4; i++)
        {
            if (Octets[i] != other.Octets[i])
            {
                return false;
            }
        }

        return true;
    }

    public bool Equals(string? other)
    {
        return string.Equals(ToString(), other);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is string stringObj) return Equals(stringObj);
        if (obj.GetType() != GetType()) return false;
        return Equals((InternetProtocolV4Address)obj);
    }

    public static bool Equals(InternetProtocolV4Address? first, InternetProtocolV4Address? second)
    {
        return first?.Equals(second) ?? second?.Equals(first) ?? true;
    }

    public static bool Equals(InternetProtocolV4Address? first, string? second)
    {
        return first?.Equals(second) ?? second == null;
    }

    public override int GetHashCode()
    {
        return Octets.GetHashCode();
    }

    public override string ToString()
    {
        return string.Join('.', Octets);
    }

    public static bool operator ==(InternetProtocolV4Address? first, InternetProtocolV4Address? second)
    {
        return Equals(first, second);
    }

    public static bool operator !=(InternetProtocolV4Address? first, InternetProtocolV4Address? second)
    {
        return !Equals(first, second);
    }

    public static bool operator ==(InternetProtocolV4Address? first, string? second)
    {
        return Equals(first, second);
    }

    public static bool operator !=(InternetProtocolV4Address? first, string? second)
    {
        return !Equals(first, second);
    }

    public static bool operator ==(string? first, InternetProtocolV4Address? second)
    {
        return Equals(second, first);
    }

    public static bool operator !=(string? first, InternetProtocolV4Address? second)
    {
        return !Equals(second, first);
    }
}