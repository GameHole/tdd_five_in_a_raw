namespace Five
{
    public class FinishNotify : Message
    {
        public int id;
        public FinishNotify()
        {
            opcode = MessageCode.FinishNotify;
        }
        public override string ToString()
        {
            return base.ToString() + " " + id;
        }
    }
}