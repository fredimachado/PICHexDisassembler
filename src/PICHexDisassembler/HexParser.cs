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
            var dataBytes = new short[wordsCount];

            for (int i = 0; i < wordsCount; i++)
            {
                var startIndex = 8 + (i * 4);

                dataBytes[i] = GetNextTwoBytesReversed(line, startIndex);
            }

            return new Hex32Record(byteCount, address, recordType, dataBytes, checksum);
        }

        private short GetNextTwoBytesReversed(string line, int startIndex)
        {
            var data = line.Substring(startIndex + 2, 2) + line.Substring(startIndex, 2);
            return short.Parse(data, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
