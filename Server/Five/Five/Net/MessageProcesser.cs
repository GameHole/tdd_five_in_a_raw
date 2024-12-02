using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Five
{
    public class MessageProcesser
    {
        public MessageContainer<IProcesser> Processers { get; private set; }
        public IProcesser defaultProcesser { get;}
        public MessageProcesser(IProcesser defaultProcesser)
        {
            this.defaultProcesser = defaultProcesser;
            Processers = new MessageContainer<IProcesser>();
        }
        public virtual void Process(ASocket socket,Message message)
        {
            if (Processers.TryGetValue(message.opcode, out var processer))
                processer.Process(socket,message);
            else
                defaultProcesser.Process(socket,message);
        }
    }
}
