namespace CodeChallenge.TomsDataOnion.Tests;

using System.Diagnostics.CodeAnalysis;
using System.Text;

using CodeChallenge.TomsDataOnion.Decoders;

public class Ascii85DecoderTests
{
    private readonly Ascii85Decoder _decoder;

    public Ascii85DecoderTests()
    {
        _decoder = new Ascii85Decoder();
    }

    [Fact]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public async Task DecodeAsync_GivenValidInput_ShouldDecodeToUtf8String()
    {
        // Arrange
        // Input taken from the Wikipedia page for Ascii85: https://en.wikipedia.org/wiki/Ascii85
        const string input = @"9jqo^BlbD-BleB1DJ+*+F(f,q/0JhKF<GL>Cj@.4Gp$d7F!,L7@<6@)/0JDEF<G%<+EV:2F!,O<
                             DJ+*.@<*K0@<6L(Df-\0Ec5e;DffZ(EZee.Bl.9pF""AGXBPCsi+DGm>@3BB/F*&OCAfu2/AKYi(
                             DIb:@FD,*)+C]U=@3BN#EcYf8ATD3s@q?d$AftVqCh[NqF<G:8+EV:.+Cf>-FD5W8ARlolDIal(
                             DId<j@<?3r@:F%a+D58'ATD4$Bl@l3De:,-DJs`8ARoFb/0JMK@qB4^F!,R<AKZ&-DfTqBG%G>u
                             D.RTpAKYo'+CT/5+Cei#DII?(E,9)oF*2M7/c";

        using var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));
        using var outputStream = new MemoryStream(input.Length);

        // Act
        await _decoder.DecodeAsync(inputStream, outputStream).ConfigureAwait(false);
        using var outputStreamReader = new StreamReader(outputStream);
        var result = await outputStreamReader.ReadToEndAsync().ConfigureAwait(false);

        // Assert
        Assert.Equal(
            "Man is distinguished, not only by his reason, but by this singular passion from other animals, which is a lust of the mind, that by a perseverance of delight in the continued and indefatigable generation of knowledge, exceeds the short vehemence of any carnal pleasure.",
            result
        );
    }
}