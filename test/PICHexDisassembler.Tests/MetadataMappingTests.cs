using PICHexDisassembler;
using Xunit;

namespace PICHexDisassembler.Tests
{
    public class MetadataMappingTests
    {
        [Fact]
        public void BankRegisterFiles_ContainsExpectedEntries()
        {
            Assert.Equal("T1CON", Instruction.BankRegisterFiles[0x10]);
            Assert.Equal("TXSTA", Instruction.BankRegisterFiles[0x98]);
            Assert.Equal("INTCON", Instruction.BankRegisterFiles[0x0B]);
        }

        [Fact]
        public void Bits_DefinesExpectedBitNamesForStatus()
        {
            var statusBits = Instruction.Bits["STATUS"];

            Assert.Equal("RP0", statusBits[5]);
            Assert.Equal("RP1", statusBits[6]);
        }

        [Fact]
        public void Bits_DefinesExpectedBitNamesForIntcon()
        {
            var intconBits = Instruction.Bits["INTCON"];

            Assert.Equal("INTF", intconBits[1]);
            Assert.Equal("GIE", intconBits[7]);
        }
    }
}

