using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayRequestProcesser : RequestProcesser
    {
        public override int OpCode => MessageCode.RequestPlay;

        protected override Response ProcessContant(AClient client, Message message)
        {
            var player = servce.domain.playerRsp.FindPlayer(client.Id);
            var result = player.Commit(message);
            return new Response().SetInfo(message, result);
        }
    }
}
