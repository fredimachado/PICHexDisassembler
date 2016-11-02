using PICHexDisassembler.Instructions;
using System;
using System.Collections.Generic;

namespace PICHexDisassembler
{
    internal class Mnemonic
    {
        private static MnemonicMapping mnemonicMapping = new MnemonicMapping
        {
            { 0x2800, 0xF800, typeof(Goto) },   // 0010100000000000
            { 0x2000, 0xF800, typeof(Call) },   // 0010000000000000
            { 0x0009, 0xFFFF, typeof(Retfie) }, // 0000000000001001
        };

        internal static Instruction Parse(byte[] dataBytes)
        {
            var data1 = dataBytes[0];
            var data2 = dataBytes[1];
            var word = (short)(data1 << 8 | data2);

            foreach (var item in mnemonicMapping)
            {
                if ((word & item.Item2) == item.Item1)
                {
                    return (Instruction)Activator.CreateInstance(item.Item3, word);
                }
            }

            return Unknown.Instance;
        }
    }
}
