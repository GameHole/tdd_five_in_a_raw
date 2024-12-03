using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ConnectProcesser : IProcesser
    {
        private App app;

        public ConnectProcesser(App app)
        {
            this.app = app;
        }

        public int OpCode => throw new NotImplementedException();

        public void Process(ASocket socket, Message message)
        {
            app.Invoke(socket);
        }
    }
}
