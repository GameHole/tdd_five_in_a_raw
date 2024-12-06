using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ConnectProcesser : IProcesser
    {
        private MatchServce app;

        public ConnectProcesser(MatchServce app)
        {
            this.app = app;
        }

        public int OpCode => throw new NotImplementedException();

        public void Process(AClient socket, Message message)
        {
            app.Login(socket);
        }
    }
}
