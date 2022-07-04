using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class NetNotifier : INotifier
    {
        private ASocket socket;
        public NetNotifier(ASocket socket)
        {
            this.socket = socket;
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
            socket.Send(new StartNotify { infos = info }); 
        }

        public void Turn(int id)
        {
            socket.Send(new PlayerIdNotify(MessageCode.TurnNotify, id));
        }
    }
}
