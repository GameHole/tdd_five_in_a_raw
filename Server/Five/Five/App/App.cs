using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class App
    {
        public MatcherMgr mgr { get; private set; }
        public GameMgr matching { get; private set; }
        public App()
        {
            matching = new GameMgr();
            mgr = new MatcherMgr(matching);
        }
        public virtual void Stop()
        {
            mgr.Stop();
            matching.Clear();
        }

        public virtual void Invoke(ASocket socket)
        {
            mgr.Invoke(socket);
        }
    }
}
