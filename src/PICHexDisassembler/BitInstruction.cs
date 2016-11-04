namespace PICHexDisassembler
{
    public abstract class BitInstruction : Instruction
    {
        private readonly ushort address;
        private readonly byte bit;

        public BitInstruction(ushort data) : base(data)
        {
            address = (ushort)(data & 0x007F);
            bit = (byte)((data & 0x0380) >> 7);
        }

        public override string ToString()
        {
            return $"{GetType().Name.ToUpper()} 0x{address:X2}, 0x{bit:X2}";
        }
    }
}
