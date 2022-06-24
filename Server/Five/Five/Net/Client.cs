using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Five
{
    public class Client
    {
        public Client(ASocket socket, Matcher matcher)
        {
            processers = new Dictionary<int, RequestProcesser>();
            Add(new MatchRequestProcesser());
            Add(new CancelRequestProcesser());
            Add(new PlayRequestProcesser());
            foreach (var item in processers.Values)
            {
                item.Init(socket, matcher);
            }
            socket.onRecv = Process;
        }
        void Add(RequestProcesser processer)
        {
            processers.Add(processer.MessageCode, processer);
        }
        Dictionary<int, RequestProcesser> processers;
        public virtual void Process(Message message)
        {
            if (processers.TryGetValue(message.opcode, out var processer))
                processer.Process(message);
        }
    }
}
