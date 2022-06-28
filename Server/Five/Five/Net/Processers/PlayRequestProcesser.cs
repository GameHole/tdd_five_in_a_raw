using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class PlayRequestProcesser : RequestProcesser
    {
        public override int MessageCode => 5;

        protected override Result processIntarnal(Message message)
        {
            PlayRequest playMessage = (PlayRequest)message;
            return matcher.Player.Play(playMessage.x, playMessage.y);
        }
    }
}
