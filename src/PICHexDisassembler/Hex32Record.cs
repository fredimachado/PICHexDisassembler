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
        }

        public int ByteCount { get; }
        public int Address { get; }
        public int RecordType { get; }
        public int[][] DataBytes { get; }
        public int Checksum { get; }
    }
}