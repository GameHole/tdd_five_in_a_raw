namespace Five
{
    public class Message
    {
        public int opcode;
        public Message() { }
        public Message(int opcode)
        {
            this.opcode = opcode;
        }
        public override string ToString()
        {
            return $"opcode:{opcode}";
        }
    }
}
