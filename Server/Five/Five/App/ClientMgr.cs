﻿using System;
using System.Collections.Concurrent;
using System.Text;

namespace Five
{
    public class ClientMgr: IApp
    {
        private Matching matching;
        public ConcurrentList<Client> clients { get; private set; }
        public RequestRegister register = new RequestRegister();
        public ClientMgr(Matching matching)
        {
            this.matching = matching;
            clients = new ConcurrentList<Client>();
        }
        public void Invoke(ASocket socket)
        {
            var client = new Client(socket, matching);
            register.Regist(client);
            clients.Add(client);
            socket.onClose += () => clients.Remove(client);
        }

        public void Stop()
        {
            clients.Clear();
        }
    }
}
