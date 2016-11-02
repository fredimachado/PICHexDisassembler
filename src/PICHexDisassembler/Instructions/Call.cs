using System;

namespace PICHexDisassembler.Instructions
{
    public class Call : Instruction
    {
        private string address;

        public Call(string data) : base(data)
        {
            address = data.Substring(3);
        }

        public override string ToString()
        {
            return "CALL 0x" + Convert.ToInt32(address, 2).ToString("X2");
        }
    }
}
