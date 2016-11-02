using Xunit;

namespace PICHexDisassembler.Tests
{
    public class HexParserTests
    {
        [Fact]
        public void ParseByteCount()
        {
            var line = ":020000000528D1";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal(2, parsed.ByteCount);
        }

        [Fact]
        public void ParseAddress()
        {
            var line = ":020000000528D1";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal(0, parsed.Address);
        }

        [Fact]
        public void ParseRecordType()
        {
            var line = ":020000000528D1";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal(0, parsed.RecordType);
        }

        [Fact]
        public void ParseChecksum()
        {
            var line = ":020000000528D1";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal(0xD1, parsed.Checksum);
        }
    }
}
