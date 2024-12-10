using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class DelayMessageProcesser : MessageProcesser
    {
        public ConcurrentQueue<Message> queue { get; } = new ConcurrentQueue<Message>();
        public DelayMessageProcesser(IProcesser defaultProcesser) : base(defaultProcesser)
        {
        }
        public override void Process(AClient socket, Message message)
        {
            queue.Enqueue(message);
        }
    }
}
