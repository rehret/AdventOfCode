namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCoreI69Emulator.Instructions;

internal sealed class SubInstruction : AbstractFixedOpCodeInstruction
{
    protected override byte OpCode => 0xC3;

    protected override void Execute(MachineState state)
    {
        IncrementProgramCounter(state, 1);
        ushort a = state.EightBitRegisters[MachineState.Registers.EightBit.A];
        ushort b = state.EightBitRegisters[MachineState.Registers.EightBit.B];

        if (b > a)
        {
            a += 256;
        }

        state.EightBitRegisters[MachineState.Registers.EightBit.A] = (byte)(a - b);
    }
}