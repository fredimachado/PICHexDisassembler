using System;

namespace PICHexDisassembler.Instructions
{
    public class Goto : Instruction
    {
        private readonly int address;

        public Goto(int data) : base(data)
        {
            address = data & 0x01FF;
        }

        public override string ToString()
        {
            return GetType().Name.ToUpper() + " 0x" + address.ToString("X2");
        }
    }
}
