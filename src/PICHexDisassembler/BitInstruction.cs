namespace PICHexDisassembler
{
    public abstract class BitInstruction : Instruction
    {
        private readonly int address;
        private readonly int bit;

        public BitInstruction(int data) : base(data)
        {
            address = data & 0x007F;
            bit = (data & 0x0380) >> 7;
        }

        public override string ToString()
        {
            return $"{GetType().Name.ToUpper()} 0x{address:X2}, 0x{bit:X2}";
        }
    }
}
