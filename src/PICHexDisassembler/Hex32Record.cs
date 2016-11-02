using PICHexDisassembler.Instructions;

namespace PICHexDisassembler
{
    public class Hex32Record
    {
        public Hex32Record(int byteCount, int address, int recordType, int[][] dataBytes, int checksum)
        {
            ByteCount = byteCount;
            Address = address;
            RecordType = recordType;
            DataBytes = dataBytes;
            Checksum = checksum;

            var wordsCount = byteCount / 2;
            Mnemonics = new Instruction[wordsCount];
            for (int i = 0; i < wordsCount; i++)
            {
                Mnemonics[i] = Mnemonic.Parse(dataBytes[i]);
            }
        }

        public int ByteCount { get; }
        public int Address { get; }
        public int RecordType { get; }
        public int[][] DataBytes { get; }
        public int Checksum { get; }

        public Instruction[] Mnemonics { get; }
    }
}
