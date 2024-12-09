namespace Five
{
    public class OpCodeErrorMessage:Message
    {
        public int unknownOpcode;
        public OpCodeErrorMessage():base(SystemOpCode.UnknownOpCode) { }
        public OpCodeErrorMessage(int unknown):this()
        {
            this.unknownOpcode = unknown;
        }
        public override string ToString()
        {
            return base.ToString() + $" unknown opcode:{unknownOpcode}";
        }
    }
}
