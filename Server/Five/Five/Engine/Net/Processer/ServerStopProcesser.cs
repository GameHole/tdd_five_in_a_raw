using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ServerStopProcesser : AServceProcesser
    {
        public override void Process(AClient socket, Message message)
        {
            servce.Stop();
        }
    }
}
