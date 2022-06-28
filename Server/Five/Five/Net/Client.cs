using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Five
{
    public class Client: MessageContainer<IProcesser>
    {
        public Client(ASocket socket)
        {
            socket.onRecv = Process;
        }
        public virtual void Process(Message message)
        {
            if (container.TryGetValue(message.opcode, out var processer))
                processer.Process(message);
        }
    }
}
