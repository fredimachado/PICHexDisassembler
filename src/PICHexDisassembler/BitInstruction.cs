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
            var register = BankRegisterFiles.ContainsKey(address) ? BankRegisterFiles[address] : $"0x{address:X2}";
            var bitDescription = Bits.ContainsKey(register) ? Bits[register][bit] : $"0x{bit:X2}";

            return $"{GetType().Name.ToUpper()} {register}, {bitDescription}";
        }
    }
}
