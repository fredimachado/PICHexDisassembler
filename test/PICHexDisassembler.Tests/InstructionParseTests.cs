using PICHexDisassembler;
using PICHexDisassembler.Instructions;
using Xunit;

namespace PICHexDisassembler.Tests
{
    public class InstructionParseTests
    {
        [Fact]
        public void Parse_Nop()
        {
            var instruction = Instruction.Parse(0x0000);

            Assert.IsType<Nop>(instruction);
            Assert.Equal("NOP", instruction.ToString());
        }

        [Fact]
        public void Parse_Return()
        {
            var instruction = Instruction.Parse(0x0008);

            Assert.IsType<Return>(instruction);
            Assert.Equal("RETURN", instruction.ToString());
        }

        [Fact]
        public void Parse_Retfie()
        {
            var instruction = Instruction.Parse(0x0009);

            Assert.IsType<Retfie>(instruction);
            Assert.Equal("RETFIE", instruction.ToString());
        }

        [Fact]
        public void Parse_Sleep()
        {
            var instruction = Instruction.Parse(0x0063);

            Assert.IsType<Sleep>(instruction);
            Assert.Equal("SLEEP", instruction.ToString());
        }

        [Fact]
        public void Parse_Clrwdt()
        {
            var instruction = Instruction.Parse(0x0064);

            Assert.IsType<Clrwdt>(instruction);
            Assert.Equal("CLRWDT", instruction.ToString());
        }

        [Fact]
        public void Parse_Call_UsesCorrectAddress()
        {
            var instruction = (Call)Instruction.Parse(0x2100);

            Assert.Equal("CALL 0x100", instruction.ToString());
        }

        [Fact]
        public void Parse_Goto_UsesCorrectAddress()
        {
            var instruction = (Goto)Instruction.Parse(0x2805);

            Assert.Equal("GOTO 0x05", instruction.ToString());
        }

        [Fact]
        public void Parse_Movlw_UsesLiteral()
        {
            var instruction = (Movlw)Instruction.Parse(0x307F);

            Assert.Equal("MOVLW 0x7F", instruction.ToString());
        }

        [Fact]
        public void Parse_Iorlw_UsesLiteral()
        {
            var instruction = (Iorlw)Instruction.Parse(0x3870);

            Assert.Equal("IORLW 0x70", instruction.ToString());
        }

        [Fact]
        public void Parse_Andlw_UsesLiteral()
        {
            var instruction = (Andlw)Instruction.Parse(0x3970);

            Assert.Equal("ANDLW 0x70", instruction.ToString());
        }

        [Fact]
        public void Parse_Xorlw_UsesLiteral()
        {
            var instruction = (Xorlw)Instruction.Parse(0x3A55);

            Assert.Equal("XORLW 0x55", instruction.ToString());
        }

        [Fact]
        public void Parse_Sublw_UsesLiteral()
        {
            var instruction = (Sublw)Instruction.Parse(0x3C01);

            Assert.Equal("SUBLW 0x01", instruction.ToString());
        }

        [Fact]
        public void Parse_Addlw_UsesLiteral()
        {
            var instruction = (Addlw)Instruction.Parse(0x3E10);

            Assert.Equal("ADDLW 0x10", instruction.ToString());
        }

        [Fact]
        public void Parse_Bsf_UsesRegisterAndBitName()
        {
            // From existing tests: BSF STATUS, RP0 (opcode 0x1683)
            var instruction = (Bsf)Instruction.Parse(0x1683);

            Assert.Equal("BSF STATUS, RP0", instruction.ToString());
        }

        [Fact]
        public void Parse_Btfss_UsesRegisterAndBitName()
        {
            // From existing tests: BTFSS INTCON, INTF (opcode 0x1C8B)
            var instruction = (Btfss)Instruction.Parse(0x1C8B);

            Assert.Equal("BTFSS INTCON, INTF", instruction.ToString());
        }

        [Fact]
        public void Parse_Movwf_WritesToNamedRegister()
        {
            // From existing tests: MOVWF 0x20 (no named register mapping, opcode 0x00A0)
            var instruction = (Movwf)Instruction.Parse(0x00A0);

            Assert.Equal("MOVWF 0x20", instruction.ToString());
        }

        [Fact]
        public void Parse_Clrf_UsesNamedRegister()
        {
            // CLRF STATUS (0x03), opcode 0x0183
            var instruction = (Clrf)Instruction.Parse(0x0183);

            Assert.Equal("CLRF STATUS", instruction.ToString());
        }

        [Fact]
        public void Parse_Subwf_WDestination()
        {
            // SUBWF 0x20, W
            var instruction = (Subwf)Instruction.Parse(0x0220);

            Assert.Contains("0x20", instruction.ToString());
            Assert.Contains("W", instruction.ToString());
        }

        [Fact]
        public void Parse_Subwf_FDestination()
        {
            // SUBWF 0x20, F (destination bit set, opcode 0x02A0)
            var instruction = (Subwf)Instruction.Parse(0x02A0);

            Assert.Contains("0x20", instruction.ToString());
            Assert.Contains("F", instruction.ToString());
        }

        [Fact]
        public void Parse_Movf_WDestination()
        {
            // MOVF 0x20, W
            var instruction = (Movf)Instruction.Parse(0x0820);

            Assert.Contains("0x20", instruction.ToString());
            Assert.Contains("W", instruction.ToString());
        }

        [Fact]
        public void Parse_Movf_FDestination()
        {
            // MOVF 0x20, F (destination bit set, opcode 0x08A0)
            var instruction = (Movf)Instruction.Parse(0x08A0);

            Assert.Contains("0x20", instruction.ToString());
            Assert.Contains("F", instruction.ToString());
        }
    }
}

