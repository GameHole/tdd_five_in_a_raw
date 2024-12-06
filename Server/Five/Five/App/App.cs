using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class App
    {
        public PlayerRepository playerRsp { get; private set; }
        public RoomRepository roomRsp { get; private set; }

        public App()
        {
            roomRsp = new RoomRepository();
            playerRsp = new PlayerRepository();
        }
        public virtual void Stop()
        {
            playerRsp.Stop();
            roomRsp.Clear();
        }

        public virtual void Login(AClient socket)
        {
            playerRsp.Login(socket);
        }
    }
}
