using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Five
{
    public class MessageProcesser: MessageContainer<IProcesser>
    {
        public IProcesser defaultProcesser { get;}

        public MessageProcesser(IProcesser defaultProcesser)
        {
            this.defaultProcesser = defaultProcesser;
        }
        public virtual void Process(AClient socket,Message message)
        {
            if (TryGetValue(message.opcode, out var processer))
                processer.Process(socket,message);
            else
                defaultProcesser.Process(socket,message);
        }
    }
}
