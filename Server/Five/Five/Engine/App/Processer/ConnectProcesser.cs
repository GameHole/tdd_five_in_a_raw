using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ConnectProcesser : AServceProcesser
    {
        public override void Process(AClient client, Message message)
        {
            var rsp = domain.playerRsp;
            var id = rsp.Login(client);
            client.Id = id;
            client.onClose += () =>
            {
                rsp.OutLine(id);
            };
        }
    }
}
