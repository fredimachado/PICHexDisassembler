using System;
using PICHexDisassembler;
using Xunit;

namespace PICHexDisassembler.Tests
{
    public class HexParserErrorTests
    {
        [Fact]
        public void ParseLine_ThrowsWhenRecordIsTooShort()
        {
            var parser = new HexParser();

            var ex = Assert.Throws<FormatException>(() => parser.ParseLine(":0000"));
            Assert.Equal("HEX record is too short.", ex.Message);
        }

        [Fact]
        public void ParseLine_ThrowsWhenByteCountDoesNotMatchDataLength()
        {
            var parser = new HexParser();

            var ex = Assert.Throws<FormatException>(() => parser.ParseLine(":0300000001FF00"));
            Assert.Equal("HEX record byte count does not match data length.", ex.Message);
        }

        [Fact]
        public void ParseLine_ThrowsWhenChecksumIsInvalid()
        {
            var parser = new HexParser();

            var ex = Assert.Throws<FormatException>(() => parser.ParseLine(":02000000000000"));
            Assert.Equal("HEX record checksum is invalid.", ex.Message);
        }

        [Fact]
        public void ParseLine_ParsesSameRecordWithAndWithoutLeadingColon()
        {
            var parser = new HexParser();
            var withColon = parser.ParseLine(":020000000528D1");
            var withoutColon = parser.ParseLine("020000000528D1");

            Assert.Equal(withColon.ByteCount, withoutColon.ByteCount);
            Assert.Equal(withColon.Address, withoutColon.Address);
            Assert.Equal(withColon.RecordType, withoutColon.RecordType);
            Assert.Equal(withColon.Checksum, withoutColon.Checksum);
            Assert.Equal(withColon.DataBytes, withoutColon.DataBytes);
        }

        [Fact]
        public void ParseLine_ParsesZeroByteCountDataRecordWithNoInstructions()
        {
            // ByteCount = 0, Address = 0x0000, RecordType = Data, no data, checksum = 0x00
            var line = ":0000000000";
            var parser = new HexParser();
            var parsed = parser.ParseLine(line);

            Assert.Equal(0, parsed.ByteCount);
            Assert.Equal(RecordType.Data, parsed.RecordType);
            Assert.Empty(parsed.Instructions);
        }
    }
}

