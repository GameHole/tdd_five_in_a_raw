using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    abstract class RequestProcesser
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
        protected abstract Result processIntarnal(Message message);
    }
}
