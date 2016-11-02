namespace PICHexDisassembler.Instructions
{
    public class Retfie : Instruction
    {
        public Retfie(int data) : base(data)
        {
        }

        public override string ToString()
        {
            return "RETFIE";
        }
    }
}
