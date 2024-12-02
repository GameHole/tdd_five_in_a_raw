using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class App
    {
        public ClientMgr mgr { get; private set; }
        public Matching matching { get; private set; }
        public App()
        {
            matching = new Matching();
            mgr = new ClientMgr(matching);
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
