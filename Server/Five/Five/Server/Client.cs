using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class Client
    {
        public ASocket socket { get; private set; }
        public MessageProcesser processer { get; private set; }
        public void Init(ASocket socket)
        {
            this.socket = socket;
            processer = new MessageProcesser(new OpCodeErrorResponseProcesser(socket));
            socket.onRecv = onRecv;
        }
        void onRecv(Message message)
        {
            processer.Process(message);
        }
    }
}
