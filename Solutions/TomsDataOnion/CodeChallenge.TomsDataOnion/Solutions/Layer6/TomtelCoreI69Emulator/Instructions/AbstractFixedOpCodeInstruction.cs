namespace CodeChallenge.TomsDataOnion.Solutions.Layer6.TomtelCoreI69Emulator.Instructions;

internal abstract class AbstractFixedOpCodeInstruction : Instruction
{
    protected abstract byte OpCode { get; }

    protected override bool CheckOpCode(byte opCode) => opCode == OpCode;
}