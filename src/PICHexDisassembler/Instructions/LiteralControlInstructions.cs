namespace PICHexDisassembler.Instructions
{
    public class Nop : Instruction
    {
        public Nop(ushort data) : base(data) { }

        public override string ToString() => "NOP";
    }

    public class Return : Instruction
    {
        public Return(ushort data) : base(data) { }

        public override string ToString() => "RETURN";
    }

    public class Sleep : Instruction
    {
        public Sleep(ushort data) : base(data) { }

        public override string ToString() => "SLEEP";
    }

    public class Retlw : Instruction
    {
        private readonly ushort literal;

        public Retlw(ushort data) : base(data)
        {
            literal = (ushort)(data & 0x00FF);
        }

        public override string ToString()
        {
            return $"RETLW 0x{literal:X2}";
        }
    }

    public class Iorlw : Instruction
    {
        private readonly ushort literal;

        public Iorlw(ushort data) : base(data)
        {
            literal = (ushort)(data & 0x00FF);
        }

        public override string ToString()
        {
            return $"IORLW 0x{literal:X2}";
        }
    }

    public class Andlw : Instruction
    {
        private readonly ushort literal;

        public Andlw(ushort data) : base(data)
        {
            literal = (ushort)(data & 0x00FF);
        }

        public override string ToString()
        {
            return $"ANDLW 0x{literal:X2}";
        }
    }

    public class Xorlw : Instruction
    {
        private readonly ushort literal;

        public Xorlw(ushort data) : base(data)
        {
            literal = (ushort)(data & 0x00FF);
        }

        public override string ToString()
        {
            return $"XORLW 0x{literal:X2}";
        }
    }

    public class Sublw : Instruction
    {
        private readonly ushort literal;

        public Sublw(ushort data) : base(data)
        {
            literal = (ushort)(data & 0x00FF);
        }

        public override string ToString()
        {
            return $"SUBLW 0x{literal:X2}";
        }
    }

    public class Addlw : Instruction
    {
        private readonly ushort literal;

        public Addlw(ushort data) : base(data)
        {
            literal = (ushort)(data & 0x00FF);
        }

        public override string ToString()
        {
            return $"ADDLW 0x{literal:X2}";
        }
    }
}

