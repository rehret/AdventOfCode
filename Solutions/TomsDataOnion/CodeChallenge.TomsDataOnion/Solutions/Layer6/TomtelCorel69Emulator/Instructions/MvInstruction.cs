namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator.Instructions;

internal sealed class MvInstruction : Abstract8BitMoveInstruction
{
    protected override bool HasImm8Source => false;

    protected override byte GetSourceValue(MachineState state, byte threeBitSrc)
    {
        var sourceLocation = GetEightBitRegister(threeBitSrc);
        return sourceLocation == MachineState.Registers.EightBit.Pointer
            ? state.Memory[state.ThirtyTwoBitRegisters[MachineState.Registers.ThirtyTwoBit.PTR] + state.EightBitRegisters[MachineState.Registers.EightBit.C]]
            : state.EightBitRegisters[sourceLocation];
    }
}