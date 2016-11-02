using System;

namespace PICHexDisassembler.Instructions
{
    public class Goto : Instruction
    {
        private readonly string address;

        public Goto(string data) : base(data)
        {
            address = data.Substring(3);
        }

        public override string ToString()
        {
            return GetType().Name.ToUpper() + " 0x" + Convert.ToInt32(address, 2).ToString("X2");
        }
    }
}
