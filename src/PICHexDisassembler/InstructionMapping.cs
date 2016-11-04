using System;
using System.Collections.Generic;

namespace PICHexDisassembler
{
    public class InstructionMapping : List<Tuple<int, int, Type>>
    {
        public void Add(int mask, int opcodeMask, Type type)
        {
            Add(new Tuple<int, int, Type>(mask, opcodeMask, type));
        }
    }
}
