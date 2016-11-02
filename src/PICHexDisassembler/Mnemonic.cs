using PICHexDisassembler.Instructions;
using System;
using System.Collections.Generic;

namespace PICHexDisassembler
{
    internal class Mnemonic
    {
        private static Dictionary<string, Type> mnemonicMapping = new Dictionary<string, Type>
        {
            { "101", typeof(Goto) },
            { "100", typeof(Call) },
            { "00000000001001", typeof(Retfie) },
        };

        internal static Instruction Parse(int[] dataBytes)
        {
            var data1 = dataBytes[0];
            var data2 = dataBytes[1];

            var data = Convert.ToString(data1, 2).PadLeft(6, '0') + Convert.ToString(data2, 2).PadLeft(8, '0');

            foreach (var item in mnemonicMapping)
            {
                if (data.StartsWith(item.Key))
                {
                    return (Instruction)Activator.CreateInstance(item.Value, data);
                }
            }

            return Unknown.Instance;
        }
    }
}
