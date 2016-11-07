using System.Collections.Generic;

namespace PICHexDisassembler
{
    public class HexParser
    {
        public Hex32RecordCollection ParseLines(string[] lines)
        {
            var records = new Hex32RecordCollection();

            foreach (var line in lines)
            {
                records.Add(ParseLine(line));
            }

            return records;
        }

        public Hex32Record ParseLine(string line)
        {
            if (line.StartsWith(":"))
            {
                line = line.Substring(1);
            }

            var byteCount = byte.Parse(line.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            var address = ushort.Parse(line.Substring(2, 4), System.Globalization.NumberStyles.HexNumber);
            var recordType = byte.Parse(line.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            var checksum = byte.Parse(line.Substring(line.Length - 2), System.Globalization.NumberStyles.HexNumber);

            var wordsCount = byteCount / 2;
            var dataBytes = new ushort[wordsCount];

            for (int i = 0; i < wordsCount; i++)
            {
                var startIndex = 8 + (i * 4);

                dataBytes[i] = GetNextTwoBytesReversed(line, startIndex);
            }

            return new Hex32Record(byteCount, address, recordType, dataBytes, checksum);
        }

        private ushort GetNextTwoBytesReversed(string line, int startIndex)
        {
            var value = ushort.Parse(line.Substring(startIndex, 4), System.Globalization.NumberStyles.HexNumber);
            return ReverseBytes(value);
        }

        private static ushort ReverseBytes(ushort value)
        {
            return (ushort)((value & 0x00FF) << 8 | (value & 0xFF00) >> 8);
        }
    }
}
