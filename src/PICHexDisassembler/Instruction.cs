namespace PICHexDisassembler
{
    public abstract class Instruction
    {
        protected readonly int data;

        public Instruction(int data)
        {
            this.data = data;
        }
    }
}
