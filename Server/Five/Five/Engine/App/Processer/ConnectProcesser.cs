using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ConnectProcesser : AServceProcesser
    {
        public override void Process(AClient client, Message message)
        {
            var servce = domain.loginServce;
            var id = servce.Login(client);
            client.Id = id;
            client.onClose += () =>
            {
                servce.OutLine(id);
            };
        }
    }
}
