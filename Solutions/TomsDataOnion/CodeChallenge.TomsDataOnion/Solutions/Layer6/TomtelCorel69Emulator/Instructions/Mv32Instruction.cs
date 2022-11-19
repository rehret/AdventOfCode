namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator.Instructions;

internal sealed class Mv32Instruction : Abstract32BitMoveInstruction
{
    protected override bool HasImm32Source => false;

    protected override uint GetSourceValue(MachineState state, byte threeBitSrc)
    {
        return state.ThirtyTwoBitRegisters[GetThirtyTwoBitRegister(threeBitSrc)];
    }
}