using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ServerProcesser : MessageProcesser
    {
        public IProcesser connect { get;private set; }
        public IProcesser serverStop { get; set; }
        public ServerProcesser(IProcesser defaultProcesser) : base(defaultProcesser)
        {
        }
        public void OnConnect(AClient client)
        {
            connect?.Process(client, default);
        }

        public void SetConnectProcesser(IProcesser connect)
        {
            this.connect = connect;
        }

        public void OnStop()
        {
            
        }
    }
}
