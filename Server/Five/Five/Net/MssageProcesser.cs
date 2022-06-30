using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Five
{
    public class MssageProcesser
    {
        public MessageContainer<IProcesser> Processers { get; private set; }
        public MssageProcesser(ASocket socket)
        {
            socket.onRecv = Process;
            Processers = new MessageContainer<IProcesser>();
        }
        public virtual void Process(Message message)
        {
            if (Processers.TryGetValue(message.opcode, out var processer))
                processer.Process(message);
        }
    }
}
