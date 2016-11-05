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
        public void ParseInstructionBytes()
        {
            var line = ":020000000528D1";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal(0x28, parsed.Instructions[0].FirstByte);
            Assert.Equal(0x05, parsed.Instructions[0].SecondByte);
        }

        [Fact]
        public void ParseGotoInstruction()
        {
            var line = ":020000000528D1";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.IsType(typeof(Goto), parsed.Instructions[0]);
        }

        [Fact]
        public void ReturnsGotoInstructionString()
        {
            var line = ":020000000528D1";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal("GOTO 0x05", parsed.Instructions[0].ToString());
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

            Assert.Equal(0x00, parsed.Instructions[0].FirstByte);
            Assert.Equal(0x09, parsed.Instructions[0].SecondByte);

            Assert.Equal(0x20, parsed.Instructions[1].FirstByte);
            Assert.Equal(0x2C, parsed.Instructions[1].SecondByte);

            Assert.Equal(0x16, parsed.Instructions[2].FirstByte);
            Assert.Equal(0x83, parsed.Instructions[2].SecondByte);

            Assert.Equal(0x13, parsed.Instructions[3].FirstByte);
            Assert.Equal(0x03, parsed.Instructions[3].SecondByte);

            Assert.Equal(0x12, parsed.Instructions[4].FirstByte);
            Assert.Equal(0x86, parsed.Instructions[4].SecondByte);

            Assert.Equal(0x12, parsed.Instructions[5].FirstByte);
            Assert.Equal(0x83, parsed.Instructions[5].SecondByte);

            Assert.Equal(0x13, parsed.Instructions[6].FirstByte);
            Assert.Equal(0x03, parsed.Instructions[6].SecondByte);

            Assert.Equal(0x16, parsed.Instructions[7].FirstByte);
            Assert.Equal(0x86, parsed.Instructions[7].SecondByte);
        }

        [Fact]
        public void ParseRetfieInstruction()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.IsType(typeof(Retfie), parsed.Instructions[0]);
        }

        [Fact]
        public void ReturnsRetfieInstructionString()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal("RETFIE", parsed.Instructions[0].ToString());
        }

        [Fact]
        public void ParseCallInstruction()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.IsType(typeof(Call), parsed.Instructions[1]);
        }

        [Fact]
        public void ReturnsCallInstructionString()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal("CALL 0x2C", parsed.Instructions[1].ToString());
        }

        [Fact]
        public void ParseBsfInstruction()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.IsType(typeof(Bsf), parsed.Instructions[2]);
        }

        [Fact]
        public void ReturnsBsfInstructionString()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal("BSF STATUS, RP0", parsed.Instructions[2].ToString());
        }

        [Fact]
        public void ParseBcfInstruction()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.IsType(typeof(Bcf), parsed.Instructions[3]);
        }

        [Fact]
        public void ReturnsBcfInstructionString()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal("BCF STATUS, RP1", parsed.Instructions[3].ToString());
        }

        [Fact]
        public void ParseDataAndReturnsTheSourceCode()
        {
            var line = ":1000080009002C2083160313861283120313861605";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            var asm = @"RETFIE
CALL 0x2C
BSF STATUS, RP0
BCF STATUS, RP1
BCF PORTB, RB5
BCF STATUS, RP0
BCF STATUS, RP1
BSF PORTB, RB5";

            Assert.Equal(asm, parsed.ToString());
        }
    }
}
