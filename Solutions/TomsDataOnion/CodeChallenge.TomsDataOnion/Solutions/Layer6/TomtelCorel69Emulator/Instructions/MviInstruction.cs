namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator.Instructions;

internal sealed class MviInstruction : Abstract8BitMoveInstruction
{
    protected override bool HasImm8Source => true;
}