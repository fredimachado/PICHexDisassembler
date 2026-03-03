using PICHexDisassembler;
using Xunit;

namespace PICHexDisassembler.Tests
{
    public class Hex32RecordCollectionTests
    {
        [Fact]
        public void ToString_RendersRecordsInInsertionOrderWithNewlines()
        {
            var collection = new Hex32RecordCollection
            {
                new Hex32Record(0, 0x0000, (byte)RecordType.ExtendedLinearAddress, new ushort[0], 0x00),
                new Hex32Record(2, 0x0000, (byte)RecordType.Data, new[] { (ushort)0x2800 }, 0x00),
                new Hex32Record(0, 0x0000, (byte)RecordType.EndOfFile, new ushort[0], 0x00)
            };

            var output = collection.ToString();

            var expected = """
                           ORG 0x0000
                           GOTO 0x00
                           END
                           """;

            Assert.Equal(expected, output);
        }

        [Fact]
        public void ToString_DoesNotAddExtraTrailingNewline()
        {
            var collection = new Hex32RecordCollection
            {
                new Hex32Record(0, 0x0000, (byte)RecordType.EndOfFile, new ushort[0], 0x00)
            };

            var output = collection.ToString();

            Assert.Equal("END", output);
        }
    }
}

