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
    }
}
