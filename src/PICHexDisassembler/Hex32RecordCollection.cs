using System.Collections.Generic;
using System.Linq;

namespace PICHexDisassembler
{
    public class Hex32RecordCollection : List<Hex32Record>
    {
        public override string ToString()
        {
            return string.Join("\r\n", this.Select(r => r.ToString()));
        }
    }
}
