using PICHexDisassembler;
using Xunit;

namespace PICHexDisassembler.Tests
{
    public class Hex32RecordTests
    {
        [Fact]
        public void ToString_ForEndOfFileRecord_ReturnsEnd()
        {
            var record = new Hex32Record(0, 0x0000, (byte)RecordType.EndOfFile, new ushort[0], 0x00);

            Assert.Equal("END", record.ToString());
        }

        [Fact]
        public void ToString_ForExtendedLinearAddress_UsesOrgWithAddress()
        {
            var record = new Hex32Record(0, 0x1234, (byte)RecordType.ExtendedLinearAddress, new ushort[0], 0x00);

            Assert.Equal("ORG 0x1234", record.ToString());
        }

        [Fact]
        public void ToString_ForConfigWord_UsesConfigFormat()
        {
            var record = new Hex32Record(2, 0x400E, (byte)RecordType.Data, new[] { (ushort)0x3F8C }, 0x00);

            Assert.Equal("__CONFIG 0x3F8C", record.ToString());
        }

        [Fact]
        public void ToString_ForDataRecordWithZeroByteCount_HasNoLines()
        {
            var record = new Hex32Record(0, 0x0000, (byte)RecordType.Data, new ushort[0], 0x00);

            Assert.Equal(string.Empty, record.ToString());
        }

        [Fact]
        public void ToString_ForStartSegmentAddressRecord_DefaultsToInstructionsFormatting()
        {
            var record = new Hex32Record(2, 0x0000, (byte)RecordType.StartSegmentAddress, new[] { (ushort)0x0000 }, 0x00);

            var output = record.ToString();

            Assert.False(string.IsNullOrEmpty(output));
        }

        [Fact]
        public void ToString_ForStartLinearAddressRecord_DefaultsToInstructionsFormatting()
        {
            var record = new Hex32Record(2, 0x0000, (byte)RecordType.StartLinearAddress, new[] { (ushort)0x0000 }, 0x00);

            var output = record.ToString();

            Assert.False(string.IsNullOrEmpty(output));
        }

        [Fact]
        public void ToString_ForExtendedSegmentAddressRecord_DefaultsToInstructionsFormatting()
        {
            var record = new Hex32Record(2, 0x0000, (byte)RecordType.ExtendedSegmentAddress, new[] { (ushort)0x0000 }, 0x00);

            var output = record.ToString();

            Assert.False(string.IsNullOrEmpty(output));
        }
    }
}

