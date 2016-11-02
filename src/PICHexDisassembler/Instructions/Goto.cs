using System;

namespace PICHexDisassembler.Instructions
{
    public class Goto : Instruction
    {
        private string address;

        public Goto(string data)
        {
            address = data.Substring(3);
        }

        public override string ToString()
        {
            return "GOTO 0x" + Convert.ToInt32(address, 2).ToString("X2");
        }
    }
}
