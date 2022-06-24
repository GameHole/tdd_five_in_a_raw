using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public abstract class ASocket
    {
        public Action<Message> onRecv;
        public abstract void Send(Message message);
    }
}
