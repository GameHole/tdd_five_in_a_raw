﻿namespace Five
{
    public class CancelRequestProcesser : RequestProcesser
    {
        protected override Response ProcessContant(AClient socket, Message message)
        {
            return new Response().SetInfo(message, servce.Cancel(socket.Id));
        }
    }
}
