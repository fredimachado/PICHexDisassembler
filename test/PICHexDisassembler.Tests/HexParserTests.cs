using PICHexDisassembler.Instructions;
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

        [Fact]
        public void ParseDataBytes()
        {
            var line = ":020000000528D1";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal(0x28, parsed.DataBytes[0][0]);
            Assert.Equal(0x05, parsed.DataBytes[0][1]);
        }

        [Fact]
        public void ParseGotoMnemonic()
        {
            var line = ":020000000528D1";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.IsType(typeof(Goto), parsed.Mnemonics[0]);
        }

        [Fact]
        public void ParseGotoInstruction()
        {
            var line = ":020000000528D1";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal("GOTO 0x05", parsed.Mnemonics[0].ToString());
        }

        [Fact]
        public void ParseLongData()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal(16, parsed.ByteCount);
            Assert.Equal(8, parsed.Address);
            Assert.Equal(0, parsed.RecordType);

            Assert.Equal(0x00, parsed.DataBytes[0][0]);
            Assert.Equal(0x09, parsed.DataBytes[0][1]);

            Assert.Equal(0x20, parsed.DataBytes[1][0]);
            Assert.Equal(0x2C, parsed.DataBytes[1][1]);

            Assert.Equal(0x16, parsed.DataBytes[2][0]);
            Assert.Equal(0x83, parsed.DataBytes[2][1]);

            Assert.Equal(0x13, parsed.DataBytes[3][0]);
            Assert.Equal(0x03, parsed.DataBytes[3][1]);

            Assert.Equal(0x12, parsed.DataBytes[4][0]);
            Assert.Equal(0x86, parsed.DataBytes[4][1]);

            Assert.Equal(0x12, parsed.DataBytes[5][0]);
            Assert.Equal(0x83, parsed.DataBytes[5][1]);

            Assert.Equal(0x13, parsed.DataBytes[6][0]);
            Assert.Equal(0x03, parsed.DataBytes[6][1]);

            Assert.Equal(0x16, parsed.DataBytes[7][0]);
            Assert.Equal(0x86, parsed.DataBytes[7][1]);
        }

        [Fact]
        public void ParseRetfieMnemonic()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.IsType(typeof(Retfie), parsed.Mnemonics[0]);
        }

        [Fact]
        public void ReturnsRetfieToString()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal("RETFIE", parsed.Mnemonics[0].ToString());
        }

        [Fact]
        public void ParseCallMnemonic()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.IsType(typeof(Call), parsed.Mnemonics[1]);
        }

        [Fact]
        public void ReturnsCallToString()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal("CALL 0x2C", parsed.Mnemonics[1].ToString());
        }
    }
}
