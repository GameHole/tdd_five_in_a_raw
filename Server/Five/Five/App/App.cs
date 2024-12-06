using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class App
    {
        public PlayerRepository mgr { get; private set; }
        public GameMgr gameRsp { get; private set; }

        public App()
        {
            gameRsp = new GameMgr();
            mgr = new PlayerRepository();
        }
        public virtual void Stop()
        {
            mgr.Stop();
            gameRsp.Clear();
        }

        public virtual void Login(AClient socket)
        {
            mgr.Login(socket);
        }
    }
}
