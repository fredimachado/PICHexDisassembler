using PICHexDisassembler.Instructions;
using System;

namespace PICHexDisassembler
{
    public abstract class Instruction
    {
        protected readonly int data;

        public Instruction(int data)
        {
            this.data = data;
        }

        public override string ToString()
        {
            return GetType().Name.ToUpper();
        }

        private static InstructionMapping instructionMapping = new InstructionMapping
        {
            { 0x2800, 0xF800, typeof(Goto) },   // mask: 0010100000000000 opcodeMask: 1111100000000000
            { 0x2000, 0xF800, typeof(Call) },   // mask: 0010000000000000 opcodeMask: 1111100000000000
            { 0x0009, 0xFFFF, typeof(Retfie) }, // mask: 0000000000001001 opcodeMask: 1111111111111111
            { 0x1400, 0xFC00, typeof(Bsf) },    // mask: 0001010000000000 opcodeMask: 1111110000000000
            { 0x1000, 0xFC00, typeof(Bcf) },    // mask: 0001000000000000 opcodeMask: 1111110000000000
        };

        internal static Instruction Parse(short dataBytes)
        {
            foreach (var item in instructionMapping)
            {
                if ((dataBytes & item.Item2) == item.Item1)
                {
                    return (Instruction)Activator.CreateInstance(item.Item3, dataBytes);
                }
            }

            return Unknown.Instance;
        }
    }
}
