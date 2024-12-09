namespace Five
{
    public class Response:Message
    {
        public int result;
        public Response() { }
        public Response(int requestOpcode) : base(MessageCode.GetResponseCode(requestOpcode)) { }
        public virtual Response SetInfo(Message message,Result result)
        {
            opcode = MessageCode.GetResponseCode(message.opcode);
            this.result = result.code;
            return this;
        }
        public override string ToString()
        {
            return $"{base.ToString()} result:{result}";
        }
    }
}
