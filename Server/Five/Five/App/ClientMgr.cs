using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class ClientMgr
    {
        private Matching matching;
        public NetList<Client> clients { get; private set; }
        public ClientMgr(Matching matching)
        {
            this.matching = matching;
            clients = new NetList<Client>();
        }
        public void onAccept(ASocket socket)
        {
            Client client = new Client(socket);
            new RequestRegister(socket, new Matcher(matching)).Regist(client);
            clients.Add(client);
            socket.onClose += () => clients.Remove(client);
        }
    }
}
