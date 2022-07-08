namespace Five
{
    public abstract class RequestProcesser : IProcesser
    {
        ASocket socket;
        protected Matcher matcher;

        public abstract int OpCode { get; }

        public void Init(ASocket socket, Matcher matcher)
        {
            this.socket = socket;
            this.matcher = matcher;
        }
        protected abstract Response ProcessContant(Message message);

        public void Process(Message message)
        {
            socket.Send(ProcessContant(message));
        }
    }
    public abstract class RequestProcesser<T>: RequestProcesser where T:Message
    {
        protected override Response ProcessContant(Message message)
        {
           return ProcessContant(message as T);
        }
        protected abstract Response ProcessContant(T message);
    }
}
