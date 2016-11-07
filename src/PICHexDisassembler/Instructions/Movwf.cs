namespace PICHexDisassembler.Instructions
{
    public class Movwf : Instruction
    {
        private readonly ushort address;

        public Movwf(ushort data) : base(data)
        {
            address = (ushort)(data & 0x007F);
        }

        public override string ToString()
        {
            return $"MOVWF 0x{address:X2}";
        }
    }
}
