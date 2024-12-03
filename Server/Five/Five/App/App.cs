using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class App
    {
        public MatcherMgr mgr { get; private set; }
        public GameMgr gameRsp { get; private set; }
        public MatchServce matchServce { get; private set; }

        public App()
        {
            gameRsp = new GameMgr();
            mgr = new MatcherMgr();
            matchServce = new MatchServce(mgr, gameRsp);
        }
        public virtual void Stop()
        {
            mgr.Stop();
            gameRsp.Clear();
        }

        public virtual void Invoke(ASocket socket)
        {
            mgr.Login(socket);
        }
    }
}
