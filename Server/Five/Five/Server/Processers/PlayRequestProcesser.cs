using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayRequestProcesser : RequestProcesser<PlayMessage>
    {
        public override int OpCode => MessageCode.RequestPlay;

        protected override Response ProcessContant(PlayMessage message)
        {
            return new Response().SetInfo(message, matcher.Player.Play(message.x, message.y));
        }
    }
}
