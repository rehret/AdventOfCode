namespace CodeChallenge.TomsDataOnion.Decoders;

internal interface IAscii85Decoder
{
    Task DecodeAsync(Stream inputStream, Stream outputStream);
}