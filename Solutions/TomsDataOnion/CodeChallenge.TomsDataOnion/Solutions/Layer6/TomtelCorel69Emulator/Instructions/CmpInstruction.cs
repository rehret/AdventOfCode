namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator.Instructions;

internal sealed class CmpInstruction : AbstractFixedOpCodeInstruction
{
    protected override byte OpCode => 0xC1;

    protected override void Execute(MachineState state)
    {
        IncrementProgramCounter(state, 1);
        state.EightBitRegisters[MachineState.Registers.EightBit.F] = state.EightBitRegisters[MachineState.Registers.EightBit.A] == state.EightBitRegisters[MachineState.Registers.EightBit.B] ? (byte)0x00 : (byte)0x01;
    }
}
