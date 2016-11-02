namespace PICHexDisassembler
{
    public class HexParser
    {
        public Hex32Record ParseLine(string line)
        {
            if (line.StartsWith(":"))
            {
                line = line.Substring(1);
            }

            var byteCount = byte.Parse(line.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            var address = int.Parse(line.Substring(2, 4), System.Globalization.NumberStyles.HexNumber);
            var recordType = byte.Parse(line.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            var checksum = byte.Parse(line.Substring(line.Length - 2), System.Globalization.NumberStyles.HexNumber);

            var wordsCount = byteCount / 2;
            var dataBytes = new byte[wordsCount][];

            for (int i = 0; i < wordsCount; i++)
            {
                var startIndex = 8 + (i * 4);

                dataBytes[i] = GetNextTwoBytesReversed(line, startIndex);
            }

            return new Hex32Record(byteCount, address, recordType, dataBytes, checksum);
        }

        private byte[] GetNextTwoBytesReversed(string line, int startIndex)
        {
            var bytes = new byte[2];
            bytes[0] = byte.Parse(line.Substring(startIndex + 2, 2), System.Globalization.NumberStyles.HexNumber);
            bytes[1] = byte.Parse(line.Substring(startIndex, 2), System.Globalization.NumberStyles.HexNumber);
            return bytes;
        }
    }
}
