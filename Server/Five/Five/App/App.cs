using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class App:IApp
    {
        public ClientMgr mgr { get; private set; }
        public Matching matching { get; private set; }

        public App()
        {
            matching = new Matching();
            mgr = new ClientMgr(matching);
        }
        public void Stop()
        {
            mgr.Stop();
            matching.Clear();
        }

        public void Invoke(ASocket socket)
        {
            mgr.Invoke(socket); 
        }
    }
}
