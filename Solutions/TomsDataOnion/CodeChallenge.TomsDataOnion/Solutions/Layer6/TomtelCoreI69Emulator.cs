namespace CodeChallenge.TomsDataOnion.Solutions.Layer6;

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

using CodeChallenge.TomsDataOnion.Solutions.Layer6.Helpers;

public class TomtelCoreI69Emulator
{
    private readonly Dictionary<char, byte> _eightBitRegisters;
    private readonly Dictionary<string, uint> _thirtyTwoBitRegisters;

    private readonly byte[] _memory;
    private readonly byte[] _outputStream;
    private int _outputStreamIndex;

    private bool _isExecuting;
    private bool _isProgramLoaded;

    public TomtelCoreI69Emulator(int memorySize)
    {
        _eightBitRegisters = new Dictionary<char, byte>
        {
            { Registers.EightBit.A, 0x00 },
            { Registers.EightBit.B, 0x00 },
            { Registers.EightBit.C, 0x00 },
            { Registers.EightBit.D, 0x00 },
            { Registers.EightBit.E, 0x00 },
            { Registers.EightBit.F, 0x00 }
        };
        _thirtyTwoBitRegisters = new Dictionary<string, uint>
        {
            { Registers.ThirtyTwoBit.LA, 0 },
            { Registers.ThirtyTwoBit.LB, 0 },
            { Registers.ThirtyTwoBit.LC, 0 },
            { Registers.ThirtyTwoBit.LD, 0 },
            { Registers.ThirtyTwoBit.PTR, 0 },
            { Registers.ThirtyTwoBit.PC, 0 }
        };

        _memory = new byte[memorySize];
        _outputStream = new byte[memorySize];

        _isExecuting = false;
        _isProgramLoaded = false;
    }

    public void LoadProgram(byte[] program)
    {
        if (program.Length > _memory.Length)
        {
            throw new ArgumentException("Program exceeds memory size", nameof(program));
        }

        Array.Copy(program, _memory, _memory.Length);
        _isProgramLoaded = true;
    }

    public byte[] Execute()
    {
        if (!_isProgramLoaded)
        {
            throw new Exception("No program has been loaded");
        }

        _isExecuting = true;
        while (_isExecuting)
        {
            ExecuteNextInstruction();
        }

        return _outputStream[.._outputStreamIndex].ToArray();
    }

    private void ExecuteNextInstruction()
    {
        var instr = _memory[_thirtyTwoBitRegisters[Registers.ThirtyTwoBit.PC]];
        switch (instr)
        {
            case 0xC2: // ADD a <- b
            {
                IncrementProgramCounter(1);
                ushort a = _eightBitRegisters[Registers.EightBit.A];
                ushort b = _eightBitRegisters[Registers.EightBit.B];

                _eightBitRegisters[Registers.EightBit.A] = (byte)((a + b) % 256);
                break;
            }

            case 0xE1: // APTR imm8
            {
                var imm8 = GetImm8();
                IncrementProgramCounter(2);
                _thirtyTwoBitRegisters[Registers.ThirtyTwoBit.PTR] += imm8;
                break;
            }

            case 0xC1: // CMP
            {
                IncrementProgramCounter(1);
                _eightBitRegisters[Registers.EightBit.F] = _eightBitRegisters[Registers.EightBit.A] == _eightBitRegisters[Registers.EightBit.B] ? (byte)0x00 : (byte)0x01;
                break;
            }

            case 0x01: // HALT
            {
                IncrementProgramCounter(1);
                _isExecuting = false;
                break;
            }

            case 0x21: // JEZ imm32
            {
                var imm32 = GetImm32();
                IncrementProgramCounter(5);
                if (_eightBitRegisters[Registers.EightBit.F] == 0x00)
                {
                    _thirtyTwoBitRegisters[Registers.ThirtyTwoBit.PC] = imm32;
                }

                break;
            }

            case 0x22: // JNZ imm32
            {
                var imm32 = GetImm32();
                IncrementProgramCounter(5);
                if (_eightBitRegisters[Registers.EightBit.F] != 0x00)
                {
                    _thirtyTwoBitRegisters[Registers.ThirtyTwoBit.PC] = imm32;
                }

                break;
            }

            case 0x02: // OUT a
            {
                IncrementProgramCounter(1);
                _outputStream[_outputStreamIndex] = _eightBitRegisters[Registers.EightBit.A];
                _outputStreamIndex++;
                break;
            }

            case 0xC3: // SUB a <- b
            {
                IncrementProgramCounter(1);
                ushort a = _eightBitRegisters[Registers.EightBit.A];
                ushort b = _eightBitRegisters[Registers.EightBit.B];

                if (b > a)
                {
                    a += 256;
                }

                _eightBitRegisters[Registers.EightBit.A] = (byte)(a - b);
                break;
            }

            case 0xC4: // XOR a <- b
            {
                IncrementProgramCounter(1);
                _eightBitRegisters[Registers.EightBit.A] ^= _eightBitRegisters[Registers.EightBit.B];
                break;
            }

            default:
            {
                // Check for move instructions
                if ((instr & 0b01000000) > 0) // MV {dest} <- {src}
                {
                    var src = GetIntermediateMoveSource(instr);
                    var dest = GetIntermediateMoveDestination(instr);

                    char destinationLocation;

                    if (src == 0) // MVI {dest} <- imm8
                    {
                        var imm8 = GetImm8();
                        IncrementProgramCounter(2);

                        destinationLocation = GetEightBitRegister(dest);
                        if (destinationLocation == Registers.EightBit.Pointer)
                        {
                            _memory[_thirtyTwoBitRegisters[Registers.ThirtyTwoBit.PTR] + _eightBitRegisters[Registers.EightBit.C]] = imm8;
                        }
                        else
                        {
                            _eightBitRegisters[destinationLocation] = imm8;
                        }

                        break;
                    }

                    IncrementProgramCounter(1);
                    var sourceLocation = GetEightBitRegister(src);
                    var source = sourceLocation == Registers.EightBit.Pointer
                        ? _memory[_thirtyTwoBitRegisters[Registers.ThirtyTwoBit.PTR] + _eightBitRegisters[Registers.EightBit.C]]
                        : _eightBitRegisters[sourceLocation];

                    destinationLocation = GetEightBitRegister(dest);
                    if (destinationLocation == Registers.EightBit.Pointer)
                    {
                        _memory[_thirtyTwoBitRegisters[Registers.ThirtyTwoBit.PTR] + _eightBitRegisters[Registers.EightBit.C]] = source;
                    }
                    else
                    {
                        _eightBitRegisters[destinationLocation] = source;
                    }

                    break;
                }

                if ((instr & 0b10000000) > 0) // MV32 {dest} <- {src}
                {
                    var src = GetIntermediateMoveSource(instr);
                    var dest = GetIntermediateMoveDestination(instr);

                    string destinationLocation;

                    if (src == 0) // MVI32 {dest} <- imm32
                    {
                        var imm32 = GetImm32();
                        IncrementProgramCounter(5);

                        destinationLocation = GetThirtyTwoBitRegister(dest);
                        _thirtyTwoBitRegisters[destinationLocation] = imm32;

                        break;
                    }

                    IncrementProgramCounter(1);

                    var sourceLocation = GetThirtyTwoBitRegister(src);
                    destinationLocation = GetThirtyTwoBitRegister(dest);

                    _thirtyTwoBitRegisters[destinationLocation] = _thirtyTwoBitRegisters[sourceLocation];
                    break;
                }

                throw new Exception($"Unexpected instruction: 0x{BitConverter.ToString(new[] { instr })}");
            }
        }
    }

    private void IncrementProgramCounter(uint increment)
    {
        _thirtyTwoBitRegisters[Registers.ThirtyTwoBit.PC] += increment;
    }

    private byte GetImm8()
    {
        var pc = _thirtyTwoBitRegisters[Registers.ThirtyTwoBit.PC];
        return _memory[pc + 1];
    }

    private uint GetImm32()
    {
        var pc = _thirtyTwoBitRegisters[Registers.ThirtyTwoBit.PC];
        return LittleEndianBitConverter.ToUInt32(_memory[((int)pc + 1)..((int)pc + 5)].AsSpan());
    }

    private static byte GetIntermediateMoveSource(byte instr)
    {
        return (byte)(instr & 0b00000111);
    }

    private static byte GetIntermediateMoveDestination(byte instr)
    {
        return (byte)((instr & 0b00111000) >> 3);
    }

    private static char GetEightBitRegister(byte threeBitLocation)
    {
        return threeBitLocation switch
        {
            1 => Registers.EightBit.A,
            2 => Registers.EightBit.B,
            3 => Registers.EightBit.C,
            4 => Registers.EightBit.D,
            5 => Registers.EightBit.E,
            6 => Registers.EightBit.F,
            7 => Registers.EightBit.Pointer,
            _ => throw new ArgumentException("Invalid three-bit location", nameof(threeBitLocation))
        };
    }

    private static string GetThirtyTwoBitRegister(byte threeBitLocation)
    {
        return threeBitLocation switch
        {
            1 => Registers.ThirtyTwoBit.LA,
            2 => Registers.ThirtyTwoBit.LB,
            3 => Registers.ThirtyTwoBit.LC,
            4 => Registers.ThirtyTwoBit.LD,
            5 => Registers.ThirtyTwoBit.PTR,
            6 => Registers.ThirtyTwoBit.PC,
            _ => throw new ArgumentException("Invalid three-bit location", nameof(threeBitLocation))
        };
    }

    public static byte[] LoadProgramFromString(string input)
    {
        var lines = input.Split('\n');
        return lines.SelectMany(line =>
        {
            // Remove comments
            if (line.Contains('#'))
            {
                line = line[..line.IndexOf('#')];
            }

            var operands = new Regex(@"\s+").Split(line.Trim());
            return operands.Select(op => Convert.ToByte(op, 16));
        }).ToArray();
    }

    private static class Registers
    {
        public static class EightBit
        {
            public const char A = 'a';
            public const char B = 'b';
            public const char C = 'c';
            public const char D = 'd';
            public const char E = 'e';
            public const char F = 'f';

            public const char Pointer = 'x';
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static class ThirtyTwoBit
        {
            public const string LA = "la";
            public const string LB = "lb";
            public const string LC = "lc";
            public const string LD = "ld";
            public const string PTR = "ptr";
            public const string PC = "pc";
        }
    }
}