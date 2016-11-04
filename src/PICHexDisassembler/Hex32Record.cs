using System.Collections.Generic;
using PICHexDisassembler.Instructions;

namespace PICHexDisassembler
{
    public class Hex32Record
    {
        public Hex32Record(byte byteCount, ushort address, byte recordType, ushort[] dataBytes, byte checksum)
        {
            ByteCount = byteCount;
            Address = address;
            RecordType = recordType;
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
        public byte RecordType { get; }
        public ushort[] DataBytes { get; }
        public byte Checksum { get; }

        public Instruction[] Instructions { get; }
    }
}
