using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ServerStopProcesser : IProcesser
    {
        private App app;

        public ServerStopProcesser(App app)
        {
            this.app = app;
        }

        public int OpCode => throw new NotImplementedException();

        public void Process(ASocket socket, Message message)
        {
            app.Stop();
        }
    }
}
