namespace PICHexDisassembler.Instructions
{
    public class Bcf : Instruction
    {
        private readonly int address;
        private readonly int bit;

        public Bcf(int data) : base(data)
        {
            address = data & 0x007F;
            bit = (data & 0x0380) >> 7;
        }

        public override string ToString()
        {
            return $"BCF 0x{address:X2}, 0x{bit:X2}";
        }
    }
}
