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
            processers = new RequestProcesser[]
            {
                new MatchRequestProcesser(),
                new CancelRequestProcesser(),
                new PlayRequestProcesser()
            };
            foreach (var item in processers)
            {
                item.Init(socket, matcher);
            }
        }
        RequestProcesser[] processers;
        public void Process(Message message)
        {
            processers[message.opcode - 1].Process(message);
        }
    }
}
