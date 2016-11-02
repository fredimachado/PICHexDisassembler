using PICHexDisassembler.Instructions;
using System;

namespace PICHexDisassembler
{
    internal class Mnemonic
    {
        internal static Instruction Parse(int[] dataBytes)
        {
            var data1 = dataBytes[0];
            var data2 = dataBytes[1];

            var data = Convert.ToString(data1, 2).PadLeft(6, '0') + Convert.ToString(data2, 2).PadLeft(8, '0');

            if (data == "00000000001001")
            {
                return new Retfie();
            }

            return new Goto(data);
        }
    }
}
