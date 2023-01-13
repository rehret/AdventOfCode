namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCoreI69Emulator.Instructions;

internal sealed class Mv32Instruction : Abstract32BitMoveInstruction
{
    protected override bool HasImm32Source => false;
}