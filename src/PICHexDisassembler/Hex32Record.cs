using System.Collections.Generic;
using PICHexDisassembler.Instructions;
using System.Linq;

namespace PICHexDisassembler
{
    public class Hex32Record
    {
        public Hex32Record(byte byteCount, ushort address, byte recordType, ushort[] dataBytes, byte checksum)
        {
            ByteCount = byteCount;
            Address = address;
            RecordType = (RecordType)recordType;
            DataBytes = dataBytes;
            Checksum = checksum;

            var wordsCount = byteCount / 2;
            Instructions = new Instruction[wordsCount];
            for (int i = 0; i < wordsCount; i++)
            {
                Instructions[i] = Instruction.Parse(dataBytes[i]);
            }
        }

        public byte ByteCount { get; }
        public int Address { get; }
        public RecordType RecordType { get; }
        public ushort[] DataBytes { get; }
        public byte Checksum { get; }

        public Instruction[] Instructions { get; }

        public override string ToString()
        {
            if (RecordType == RecordType.ExtendedLinearAddress)
            {
                return $"ORG 0x{Address:X4}";
            }

            return string.Join("\r\n", Instructions.Select(m => m.ToString()));
        }
    }

    public enum RecordType
    {
        Data = 0,
        EndOfFile,
        ExtendedSegmentAddress,
        StartSegmentAddress,
        ExtendedLinearAddress,
        StartLinearAddress
    }
}
