namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCorel69Emulator.Instructions;

// ReSharper disable once IdentifierTypo
internal sealed class AptrInstruction : AbstractFixedOpCodeInstruction
{
    protected override byte OpCode => 0xE1;

    protected override void Execute(MachineState state)
    {
        var imm8 = GetImm8(state);
        IncrementProgramCounter(state, 2);
        state.ThirtyTwoBitRegisters[MachineState.Registers.ThirtyTwoBit.PTR] += imm8;
    }
}
