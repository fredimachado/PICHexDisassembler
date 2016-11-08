using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PICHexDisassembler.Instructions
{
    public class Movlw : Instruction
    {
        private readonly ushort literal;

        public Movlw(ushort data) : base(data)
        {
            literal = (ushort)(data & 0x00FF);
        }

        public override string ToString()
        {
            return $"MOVLW 0x{literal:X2}";
        }
    }
}
