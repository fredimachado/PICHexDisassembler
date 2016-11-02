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

            var byteCount = int.Parse(line.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            var address = int.Parse(line.Substring(2, 4), System.Globalization.NumberStyles.HexNumber);
            var recordType = int.Parse(line.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            var checksum = int.Parse(line.Substring(line.Length - 2), System.Globalization.NumberStyles.HexNumber);

            var dataBytes = new int[1][];

            dataBytes[0] = new[]
            {
                int.Parse(line.Substring(8 + 2, 2), System.Globalization.NumberStyles.HexNumber),
                int.Parse(line.Substring(8, 2), System.Globalization.NumberStyles.HexNumber)
            };

            return new Hex32Record(byteCount, address, recordType, dataBytes, checksum);
        }
    }
}
