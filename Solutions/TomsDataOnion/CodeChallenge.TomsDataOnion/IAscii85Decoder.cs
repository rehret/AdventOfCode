namespace CodeChallenge.TomsDataOnion;

internal interface IAscii85Decoder
{
    Task DecodeAsync(Stream inputStream, Stream outputStream);
}