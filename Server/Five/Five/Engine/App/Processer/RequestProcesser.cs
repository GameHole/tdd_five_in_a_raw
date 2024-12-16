namespace Five
{
    public abstract class AServceProcesser : IProcesser
    {
        protected Domain domain;
        public void Init(Domain domain)
        {
            this.domain = domain;
        }
        public abstract void Process(AClient socket, Message message);
    }
    public abstract class RequestProcesser : AServceProcesser
    {
        protected abstract Response ProcessContant(AClient socket,Message message);

        public override void Process(AClient socket, Message message)
        {
            socket.Send(ProcessContant(socket,message));
        }
    }
    public abstract class RequestProcesser<T>: RequestProcesser where T:Message
    {
        protected override Response ProcessContant(AClient socket, Message message)
        {
           return ProcessContant(socket,message as T);
        }
        protected abstract Response ProcessContant(AClient socket, T message);
    }
}
