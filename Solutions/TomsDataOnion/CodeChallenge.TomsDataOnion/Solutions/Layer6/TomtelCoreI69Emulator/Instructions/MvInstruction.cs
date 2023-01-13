namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCoreI69Emulator.Instructions;

internal sealed class MvInstruction : Abstract8BitMoveInstruction
{
    protected override bool HasImm8Source => false;
}