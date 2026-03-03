using PICHexDisassembler;
using PICHexDisassembler.Instructions;
using Xunit;

namespace PICHexDisassembler.Tests
{
    public class UnknownInstructionTests
    {
        [Fact]
        public void Parse_ReturnsUnknownForUnmappedOpcode()
        {
            // Choose an opcode that is not present in the mapping table but matches an existing mask
            // Unknown.Instance is returned when no mapping type is found
            var instruction = Instruction.Parse(0xFFFF);

            Assert.Same(Unknown.Instance, instruction);
        }

        [Fact]
        public void Hex32Record_ToString_DoesNotThrowForUnknownInstruction()
        {
            var dataBytes = new[] { (ushort)0xFFFF };
            var record = new Hex32Record(2, 0x0000, (byte)RecordType.Data, dataBytes, 0x00);

            var output = record.ToString();

            Assert.NotNull(output);
        }
    }
}

