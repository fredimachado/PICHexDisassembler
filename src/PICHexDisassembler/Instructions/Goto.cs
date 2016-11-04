using System;

namespace PICHexDisassembler.Instructions
{
    public class Goto : Instruction
    {
        private readonly ushort address;

        public Goto(ushort data) : base(data)
        {
            address = (ushort)(data & 0x01FF);
        }

        public override string ToString()
        {
            return GetType().Name.ToUpper() + " 0x" + address.ToString("X2");
        }
    }
}
