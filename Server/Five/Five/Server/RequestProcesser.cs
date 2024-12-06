﻿namespace Five
{
    public abstract class RequestProcesser : IProcesser
    {
        protected MatchServce servce;

        public abstract int OpCode { get; }

        public void Init(MatchServce app)
        {
            this.servce = app;
        }
        protected abstract Response ProcessContant(AClient socket,Message message);

        public void Process(AClient socket, Message message)
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
