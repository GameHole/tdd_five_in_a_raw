using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public abstract class RequestProcesser: IProcesser
    {
        ASocket socket;
        protected Matcher matcher;
        public void Init(ASocket socket, Matcher matcher)
        {
            this.socket = socket;
            this.matcher = matcher;
        }
        public void Process(Message message)
        {
            socket.Send(new Response(message, processIntarnal(message)));
        }
        public abstract int OpCode { get; }
        protected abstract Result processIntarnal(Message message);
    }
}
