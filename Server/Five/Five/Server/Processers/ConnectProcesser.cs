using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ConnectProcesser : AServceProcesser
    {
        public override int OpCode { get; }
        public override void Process(AClient socket, Message message)
        {
            servce.Login(socket);
        }
    }
}
