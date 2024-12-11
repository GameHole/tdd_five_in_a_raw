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
        public void Finish(int id)
        {
            socket.Send(new PlayerIdNotify(MessageCode.FinishNotify, id));
        }

        public void Played(int x, int y, int id)
        {
            socket.Send(new PlayedNotify { x = x, y = y, id = id });
        }

        public void Start(PlayerInfo[] info)
        {
            socket.Send(new StartNotify { playerId = player.PlayerId, infos = info });
        }


        public void Turn(int id)
        {
            socket.Send(new PlayerIdNotify(MessageCode.TurnNotify, id));
        }
    }
}
