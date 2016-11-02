namespace PICHexDisassembler.Instructions
{
    public class Retfie : Instruction
    {
        public Retfie(string data) : base(data)
        {
        }

        public override string ToString()
        {
            return "RETFIE";
        }
    }
}
