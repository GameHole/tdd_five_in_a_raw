using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class App
    {
        public Server server { get; private set; }

        public App(Server server)
        {
            this.server = server;
            matching = new Matching();
            mgr = new ClientMgr(matching);
            server.onAccept += mgr.onAccept;
        }

        public ClientMgr mgr { get;private set; }
        public Matching matching { get; private set; }
        public void StartAsync()
        {
            server.StartAsync();
        }

        public void Stop()
        {
            mgr.Clear();
            server.Stop();
            matching.Clear();
        }
    }
}
