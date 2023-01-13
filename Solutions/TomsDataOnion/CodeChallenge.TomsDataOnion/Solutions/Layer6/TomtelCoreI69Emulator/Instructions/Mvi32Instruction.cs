namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCoreI69Emulator.Instructions;

internal sealed class Mvi32Instruction : Abstract32BitMoveInstruction
{
    protected override bool HasImm32Source => true;
}