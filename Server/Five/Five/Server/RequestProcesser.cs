namespace Five
{
    public abstract class RequestProcesser : IProcesser
    {
        protected MatcherMgr mgr;

        public abstract int OpCode { get; }

        public void Init(MatcherMgr matcher)
        {
            this.mgr = matcher;
        }
        protected abstract Response ProcessContant(ASocket socket,Message message);

        public void Process(ASocket socket, Message message)
        {
            socket.Send(ProcessContant(socket,message));
        }
    }
    public abstract class RequestProcesser<T>: RequestProcesser where T:Message
    {
        protected override Response ProcessContant(ASocket socket, Message message)
        {
           return ProcessContant(socket,message as T);
        }
        protected abstract Response ProcessContant(ASocket socket, T message);
    }
}
