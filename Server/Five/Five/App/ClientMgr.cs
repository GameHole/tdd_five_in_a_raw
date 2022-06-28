using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ClientMgr
    {
        private List<Client> clients;
        private Matching matching;

        public ClientMgr(Matching matching)
        {
            this.matching = matching;
            clients = new List<Client>();
        }

        public int ClientCount { get=> clients.Count;}

        public void onAccept(ASocket socket)
        {
            Client client = new Client(socket);
            new RequestRegister(socket, new Matcher(matching)).Regist(client);
            clients.Add(client);
            socket.onClose += () => Remove(client);
        }

        public void Remove(Client socket)
        {
            clients.Remove(socket);
        }

        public void Clear()
        {
            clients.Clear();
        }

        public Client GetClient(int index)
        {
            return clients[index];
        }
    }
}
