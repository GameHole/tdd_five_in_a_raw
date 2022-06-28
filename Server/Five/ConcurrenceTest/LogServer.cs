using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConcurrenceTest
{
    class LogServer : Server
    {
        internal string log;

        public LogServer(string ip, int port) : base(ip, port) { }
        public override void Stop()
        {
            try
            {
                base.Stop();
            }
            catch (Exception e)
            {
                log = e.ToString();
            }
        }
    }
}
