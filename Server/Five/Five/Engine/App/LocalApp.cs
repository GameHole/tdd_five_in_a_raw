using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class ServceClient : AClient
    {
        private MessageProcesser clientProcesser;
        private AClient client;

        public ServceClient(AClient client, MessageProcesser clientProcesser)
        {
            this.client = client;
            this.clientProcesser = clientProcesser;
        }

        public override void Send(Message message)
        {
            clientProcesser.Process(client, message);
        }
    }
    public class LocalApp:AClient
    {
        private ServceClient serverClient;
        private ServerProcesser serverProcesser;

        public LocalApp(MessageProcesser clientProcesser, ServerProcesser serverProcesser)
        {
            serverClient = new ServceClient(this,clientProcesser);
            this.serverProcesser = serverProcesser;
            onClose += () => serverProcesser.serverStop.Process(default, default);
        }

        public void Start()
        {
            serverProcesser.OnConnect(serverClient);
        }

        public override void Send(Message msg)
        {
            serverProcesser.Process(serverClient,msg);
        }
    }
}
