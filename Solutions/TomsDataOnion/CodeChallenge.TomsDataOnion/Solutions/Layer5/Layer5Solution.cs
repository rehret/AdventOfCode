namespace CodeChallenge.TomsDataOnion.Solutions.Layer5;

using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

[TomsDataOnionSolution(5)]
internal class Layer5Solution : TomsDataOnionSolution
{
    public Layer5Solution(IInputProvider<TomsDataOnionChallengeSelection, byte> inputProvider, ITomsDataOnionOutputWriter outputWriter)
        : base(inputProvider, outputWriter)
    { }

    protected override IEnumerable<byte> Decode(IEnumerable<byte> input)
    {
        var inputParts = GetInputParts(input.ToArray());
        var aesKey = DecryptKey(inputParts.WrappedKey!, inputParts.AesKek, inputParts.AesKekIV!);
        return Decrypt(inputParts.Payload!, aesKey, inputParts.AesIV!);
    }

    private static InputParts GetInputParts(byte[] bytes)
    {
        return new InputParts
        {
            AesKek = bytes[..32],
            AesKekIV = bytes[32..(32 + 8)],
            WrappedKey = bytes[(32 + 8)..(32 + 8 + 40)],
            AesIV = bytes[(32 + 8 + 40)..(32 + 8 + 40 + 16)],
            Payload = bytes[(32 + 8 + 40 + 16)..]
        };
    }

    private static byte[] DecryptKey(byte[] encryptedKey, byte[]? kek, byte[] iv)
    {
        var engine = new AesWrapEngine();
        engine.Init(false, new ParametersWithIV(new KeyParameter(kek), iv));
        return engine.Unwrap(encryptedKey, 0, encryptedKey.Length);
    }

    private static IEnumerable<byte> Decrypt(byte[] payload, byte[] key, byte[] iv)
    {
        var cipher = CipherUtilities.GetCipher("AES/CTR/NOPADDING");
        cipher.Init(false, new ParametersWithIV(new KeyParameter(key), iv));
        return cipher.DoFinal(payload);
    }

    private class InputParts
    {
        public byte[]? AesKek { get; init; }

        public byte[]? AesKekIV { get; init; }

        public byte[]? WrappedKey { get; init; }

        public byte[]? AesIV { get; init; }

        public byte[]? Payload { get; init; }
    }
}