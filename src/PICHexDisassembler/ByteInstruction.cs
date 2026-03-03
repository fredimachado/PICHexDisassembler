namespace PICHexDisassembler
{
    public abstract class ByteInstruction : Instruction
    {
        protected readonly ushort address;
        protected readonly byte destination;

        protected ByteInstruction(ushort data) : base(data)
        {
            address = (ushort)(data & 0x007F);
            destination = (byte)((data & 0x0080) >> 7);
        }

        protected string GetRegisterName()
        {
            return BankRegisterFiles.ContainsKey(address) ? BankRegisterFiles[address] : $"0x{address:X2}";
        }

        protected string GetDestinationName()
        {
            return Registers.ContainsKey(destination) ? Registers[destination] : destination.ToString();
        }
    }
}

