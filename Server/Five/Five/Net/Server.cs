using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Five
{
    public class Server
    {
        public Socket socket { get;private set; }
        public int SocketCount { get=> clients.Count;  }
        public Action<TcpSocket> onAccept;

        public bool IsRun { get; private set; }
        private List<TcpSocket> clients = new List<TcpSocket>();
        public Server(string ip,int port)
        {
            socket = new Socket(SocketType.Stream,ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
        }
        internal void Remove(TcpSocket socket)
        {
            clients.Remove(socket);
        }
        public bool Contains(TcpSocket socket)
        {
            return clients.Contains(socket);
        }
        public void Stop()
        {
            foreach (var item in clients)
            {
                item.Release();
            }
            clients.Clear();
            socket.Close();
            IsRun = false;
        }

        public void StartAsync()
        {
            Task.Factory.StartNew(() =>
            {
                IsRun = true;
                socket.Listen(20);
                while (IsRun)
                {
                    var client = socket.Accept();
                    var tcp = new TcpServerSocket(client,this);
                    clients.Add(tcp);
                    onAccept?.Invoke(tcp);
                }
            });
        }
    }
}
