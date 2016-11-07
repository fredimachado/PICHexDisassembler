namespace PICHexDisassembler.Instructions
{
    public class Swapf : Instruction
    {
        private readonly ushort address;
        private readonly byte destination;

        public Swapf(ushort data) : base(data)
        {
            address = (ushort)(data & 0x007F);
            destination = (byte)((data & 0x0040) >> 6);
        }

        public override string ToString()
        {
            var register = BankRegisterFiles[address] ?? $"0x{address:X2}";
            var destinationRegister = Registers[destination] ?? destination.ToString();

            return $"SWAPF {register}, {destinationRegister}";
        }
    }
}
