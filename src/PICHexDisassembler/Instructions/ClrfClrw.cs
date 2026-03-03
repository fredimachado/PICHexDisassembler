namespace PICHexDisassembler.Instructions
{
    public class Clrf : ByteInstruction
    {
        public Clrf(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"CLRF {GetRegisterName()}";
        }
    }

    public class Clrw : Instruction
    {
        public Clrw(ushort data) : base(data) { }

        public override string ToString()
        {
            return "CLRW";
        }
    }
}

