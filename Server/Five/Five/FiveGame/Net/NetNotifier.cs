using Five.RTS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class NetNotifier : INotifier
    {
        private AClient socket;
        private Player player;
        public NetNotifier(AClient socket,Player player)
        {
            this.socket = socket;
            this.player = player;
        }
        public void Finish(PlayerIdNotify  notify)
        {
            socket.Send(notify);
        }

        public void Played(PlayedNotify notify)
        {
            socket.Send(notify);
        }

        public void Start(StartNotify notify)
        {
            socket.Send(notify);
        }


        public void Turn(PlayerIdNotify notify)
        {
            socket.Send(notify);
        }
    }
}
