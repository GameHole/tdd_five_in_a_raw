using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public abstract class ASocket
    {
        public Action<Message> onRecv;
        public Action onClose;
        public bool isVailed { get; protected set; }
        public abstract void Connect(string ip, int port);
        public abstract void Send(Message message);
        public abstract void Close();
    }
}
