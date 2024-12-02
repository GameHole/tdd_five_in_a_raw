﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayRequestProcesser : RequestProcesser<PlayMessage>
    {
        public override int OpCode => MessageCode.RequestPlay;

        protected override Response ProcessContant(ASocket socket, PlayMessage message)
        {
            int x = message.x;
            int y = message.y;
            var sok = socket;
            return new Response().SetInfo(message, mgr.Play(x, y, sok));
        }
    }
}
