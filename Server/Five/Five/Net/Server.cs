using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Five
{
    public class Server
    {
        public List<TcpClient> clients = new List<TcpClient>();

        public TcpListener Listener { get;private set; }

        public Server(string ip,int port)
        {
            Listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip), port));
        }
        public void Stop()
        {
            foreach (var item in clients)
            {
                item.Close();
            }
            clients.Clear();
        }

        public async void Start()
        {
            Listener.Start();
            while (true)
            {
                var client = await Listener.AcceptTcpClientAsync();
                clients.Add(client);
            }
        }
    }
}
