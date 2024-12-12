using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ConnectProcesser : AServceProcesser
    {
        public override int OpCode { get; }
        public override void Process(AClient client, Message message)
        {
            var rsp = servce.domain.playerRsp;
            rsp.Login(client);
        }
    }
}
