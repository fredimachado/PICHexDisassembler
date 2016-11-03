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

            Assert.Equal(0x28, parsed.GetFirstByteFromIndex(0));
            Assert.Equal(0x05, parsed.GetSecondByteFromIndex(0));
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

            Assert.Equal(0x00, parsed.GetFirstByteFromIndex(0));
            Assert.Equal(0x09, parsed.GetSecondByteFromIndex(0));

            Assert.Equal(0x20, parsed.GetFirstByteFromIndex(1));
            Assert.Equal(0x2C, parsed.GetSecondByteFromIndex(1));

            Assert.Equal(0x16, parsed.GetFirstByteFromIndex(2));
            Assert.Equal(0x83, parsed.GetSecondByteFromIndex(2));

            Assert.Equal(0x13, parsed.GetFirstByteFromIndex(3));
            Assert.Equal(0x03, parsed.GetSecondByteFromIndex(3));

            Assert.Equal(0x12, parsed.GetFirstByteFromIndex(4));
            Assert.Equal(0x86, parsed.GetSecondByteFromIndex(4));

            Assert.Equal(0x12, parsed.GetFirstByteFromIndex(5));
            Assert.Equal(0x83, parsed.GetSecondByteFromIndex(5));

            Assert.Equal(0x13, parsed.GetFirstByteFromIndex(6));
            Assert.Equal(0x03, parsed.GetSecondByteFromIndex(6));

            Assert.Equal(0x16, parsed.GetFirstByteFromIndex(7));
            Assert.Equal(0x86, parsed.GetSecondByteFromIndex(7));
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

        [Fact]
        public void ParseBsfMnemonic()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.IsType(typeof(Bsf), parsed.Mnemonics[2]);
        }

        [Fact]
        public void ReturnsBsfToString()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal("BSF 0x03, 0x05", parsed.Mnemonics[2].ToString());
        }

        [Fact]
        public void ParseBcfMnemonic()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.IsType(typeof(Bcf), parsed.Mnemonics[3]);
        }

        [Fact]
        public void ReturnsBcfToString()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal("BCF 0x03, 0x06", parsed.Mnemonics[3].ToString());
        }
    }
}
