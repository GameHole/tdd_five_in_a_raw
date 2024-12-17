using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayRequestProcesser : RequestProcesser
    {
        protected override Response ProcessContant(AClient client, Message message)
        {
            Result result = domain.playServce.Commit(client.Id, message);
            return new Response().SetInfo(message, result);
        }

       
    }
}
