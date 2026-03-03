namespace PICHexDisassembler.Instructions
{
    public class Subwf : ByteInstruction
    {
        public Subwf(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"SUBWF {GetRegisterName()}, {GetDestinationName()}";
        }
    }

    public class Decf : ByteInstruction
    {
        public Decf(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"DECF {GetRegisterName()}, {GetDestinationName()}";
        }
    }

    public class Iorwf : ByteInstruction
    {
        public Iorwf(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"IORWF {GetRegisterName()}, {GetDestinationName()}";
        }
    }

    public class Andwf : ByteInstruction
    {
        public Andwf(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"ANDWF {GetRegisterName()}, {GetDestinationName()}";
        }
    }

    public class Xorwf : ByteInstruction
    {
        public Xorwf(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"XORWF {GetRegisterName()}, {GetDestinationName()}";
        }
    }

    public class Addwf : ByteInstruction
    {
        public Addwf(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"ADDWF {GetRegisterName()}, {GetDestinationName()}";
        }
    }

    public class Movf : ByteInstruction
    {
        public Movf(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"MOVF {GetRegisterName()}, {GetDestinationName()}";
        }
    }

    public class Comf : ByteInstruction
    {
        public Comf(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"COMF {GetRegisterName()}, {GetDestinationName()}";
        }
    }

    public class Incf : ByteInstruction
    {
        public Incf(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"INCF {GetRegisterName()}, {GetDestinationName()}";
        }
    }

    public class Decfsz : ByteInstruction
    {
        public Decfsz(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"DECFSZ {GetRegisterName()}, {GetDestinationName()}";
        }
    }

    public class Rrf : ByteInstruction
    {
        public Rrf(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"RRF {GetRegisterName()}, {GetDestinationName()}";
        }
    }

    public class Rlf : ByteInstruction
    {
        public Rlf(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"RLF {GetRegisterName()}, {GetDestinationName()}";
        }
    }

    public class Incfsz : ByteInstruction
    {
        public Incfsz(ushort data) : base(data) { }

        public override string ToString()
        {
            return $"INCFSZ {GetRegisterName()}, {GetDestinationName()}";
        }
    }
}

