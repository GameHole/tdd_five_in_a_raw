using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayRequestProcesser : RequestProcesser<PlayRequest>
    {
        public override int OpCode => MessageCode.RequestPlay;

        protected override Response ProcessContant(PlayRequest message)
        {
            return new Response().SetInfo(message, matcher.Player.Play(message.x, message.y));
        }
    }
}
