namespace CodeChallenge.TomsDataOnion;

using System.Text;

using CodeChallenge.Core;

internal class TomsDataOnionInputProvider : IInputProvider<TomsDataOnionChallengeSelection, byte>
{
    private const string DatagramStart = "<~";
    private const string DatagramEnd = "~>";

    private readonly IAscii85Decoder _ascii85Decoder;

    public TomsDataOnionInputProvider(IAscii85Decoder ascii85Decoder)
    {
        _ascii85Decoder = ascii85Decoder;
    }

    public async Task<IEnumerable<byte>> GetInputAsync(TomsDataOnionChallengeSelection challengeSelection)
    {
        // Get input
        string rawInput;
        using (var fileReader = new StreamReader(GetInputFilePath(challengeSelection), Encoding.UTF8))
        {
            rawInput = await fileReader.ReadToEndAsync().ConfigureAwait(false);
        }

        // Grab the content between <~ and ~>
        var payloadStart = rawInput.IndexOf(DatagramStart, StringComparison.Ordinal);
        var payloadEnd = rawInput.LastIndexOf(DatagramEnd, StringComparison.Ordinal);
        rawInput = rawInput[payloadStart..(payloadEnd + DatagramEnd.Length)];

        // Prepare the input for Ascii85 decoding
        // The Adobe spec says that the data is contained between <~ and ~>
        // and that newlines should be ignored
        rawInput = rawInput.Trim();
        if (rawInput.StartsWith(DatagramStart))
            rawInput = rawInput[DatagramStart.Length..];
        if (rawInput.EndsWith(DatagramEnd))
            rawInput = rawInput[..^DatagramEnd.Length];

        // Ascii85-decode the input
        using var decodeStream = new MemoryStream();
        using var inputStream = new MemoryStream(rawInput.Select(x => (byte)x).ToArray());
        await _ascii85Decoder.DecodeAsync(inputStream, decodeStream).ConfigureAwait(false);
        return decodeStream.ToArray();
    }

    private static string GetInputFilePath(TomsDataOnionChallengeSelection challengeSelection) =>
        Path.Combine(
            Environment.CurrentDirectory,
            $"Resources/TomsDataOnion/Layer{challengeSelection.Layer:0}.txt"
        );
}