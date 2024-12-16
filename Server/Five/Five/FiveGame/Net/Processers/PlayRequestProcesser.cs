using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayRequestProcesser : RequestProcesser
    {
        protected override Response ProcessContant(AClient client, Message message)
        {
            Result result = Play(client.Id, message);
            return new Response().SetInfo(message, result);
        }

        private Result Play(int id, Message message)
        {
            var player = domain.playerRsp.FindPlayer(id);
            var result = player.Commit(message);
            return result;
        }
    }
}
