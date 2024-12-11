using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayRequestProcesser : RequestProcesser<PlayMessage>
    {
        public override int OpCode => MessageCode.RequestPlay;

        protected override Response ProcessContant(AClient socket, PlayMessage message)
        {
            return new Response().SetInfo(message, servce.Commit(new PlayRequest 
            { 
                x = message.x,
                y = message.y 
            }, socket));
        }
    }
}
