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
            socket.Send(new FinishNotify(id));
        }

        public void Played(int x, int y, int id)
        {
            socket.Send(new PlayedNotify(x,y,id));
        }

        public void Start(PlayerInfo[] info)
        {
            socket.Send(new StartNotify(info));
        }
    }
}
