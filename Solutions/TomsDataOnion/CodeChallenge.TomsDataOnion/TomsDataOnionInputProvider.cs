namespace CodeChallenge.TomsDataOnion;

using System.Text;

using CodeChallenge;

internal class TomsDataOnionInputProvider : IInputProvider<TomsDataOnionChallengeSelection, byte>
{
    public async Task<IEnumerable<byte>> GetInputAsync(TomsDataOnionChallengeSelection challengeSelection)
    {
        // Get input
        string rawInput;
        using (var fileReader = new StreamReader(GetInputFilePath(challengeSelection), Encoding.UTF8))
        {
            rawInput = await fileReader.ReadToEndAsync().ConfigureAwait(false);
        }

        // Grab the content between <~ and ~>
        var payloadStart = rawInput.IndexOf(Constants.DatagramStart, StringComparison.Ordinal);
        var payloadEnd = rawInput.LastIndexOf(Constants.DatagramEnd, StringComparison.Ordinal);
        rawInput = rawInput[payloadStart..(payloadEnd + Constants.DatagramEnd.Length)];

        // Prepare the input for Ascii85 decoding
        // The Adobe spec says that the data is contained between <~ and ~>
        // and that newlines should be ignored
        rawInput = rawInput.Trim();
        if (rawInput.StartsWith(Constants.DatagramStart))
            rawInput = rawInput[Constants.DatagramStart.Length..];
        if (rawInput.EndsWith(Constants.DatagramEnd))
            rawInput = rawInput[..^Constants.DatagramEnd.Length];
        rawInput = rawInput
            .Replace("\r", string.Empty)
            .Replace("\n", string.Empty);

        // Ascii85-decode the input
        using (var decodeStream = new MemoryStream())
        using (var textReader = new StringReader(rawInput))
        {
            await SimpleBase.Base85.Ascii85.DecodeAsync(textReader, decodeStream).ConfigureAwait(false);
            return decodeStream.ToArray();
        }
    }

    private static string GetInputFilePath(TomsDataOnionChallengeSelection challengeSelection) =>
        Path.Combine(
            Environment.CurrentDirectory,
            $"Resources/TomsDataOnion/Layer{challengeSelection.Layer:0}.txt"
        );
}