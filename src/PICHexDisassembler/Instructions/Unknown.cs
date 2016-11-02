namespace PICHexDisassembler.Instructions
{
    public sealed class Unknown : Instruction
    {
        private static Unknown instance;

        private Unknown() : base(0)
        {
        }

        static Unknown()
        {
            instance = new Unknown();
        }

        public static Unknown Instance => instance;
    }
}
