namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCoreI69Emulator.Instructions;

internal sealed class AddInstruction : AbstractFixedOpCodeInstruction
{
    protected override byte OpCode => 0xC2;

    protected override void Execute(MachineState state)
    {
        IncrementProgramCounter(state, 1);
        ushort a = state.EightBitRegisters[MachineState.Registers.EightBit.A];
        ushort b = state.EightBitRegisters[MachineState.Registers.EightBit.B];

        state.EightBitRegisters[MachineState.Registers.EightBit.A] = (byte)((a + b) % 256);
    }
}
