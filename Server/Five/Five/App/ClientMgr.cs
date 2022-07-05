using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class ClientMgr
    {
        private Matching matching;
        public ConcurrentList<MessageProcesser> clients { get; private set; }
        public ClientMgr(Matching matching)
        {
            this.matching = matching;
            clients = new ConcurrentList<MessageProcesser>();
        }
        public void onAccept(ASocket socket)
        {
            MessageProcesser client = new MessageProcesser(socket,new OpCodeErrorResponseProcesser(socket));
            new RequestRegister(socket, new Matcher(matching)).Regist(client);
            clients.Add(client);
            socket.onClose += () => clients.Remove(client);
        }
    }
}
