namespace PICHexDisassembler
{
    public abstract class Instruction
    {
        protected readonly string data;

        public Instruction(string data)
        {
            this.data = data;
        }
    }
}
