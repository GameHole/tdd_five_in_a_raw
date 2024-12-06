using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ServerStopProcesser : IProcesser
    {
        private MatchServce app;

        public ServerStopProcesser(MatchServce app)
        {
            this.app = app;
        }

        public int OpCode => throw new NotImplementedException();

        public void Process(AClient socket, Message message)
        {
            app.Stop();
        }
    }
}
