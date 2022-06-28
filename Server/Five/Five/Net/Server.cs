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
        public NetList<TcpSocket> sockets { get; private set; }
        public Action<TcpSocket> onAccept;
        public bool IsRun { get; private set; }
        public Server(string ip,int port)
        {
            socket = new Socket(SocketType.Stream,ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            sockets = new NetList<TcpSocket>();
            sockets.onAdd = (s) => onAccept?.Invoke(s);
        }
        public virtual void Stop()
        {
            foreach (var item in sockets)
            {
                item.Release();
            }
            sockets.Clear();
            socket.Close();
            IsRun = false;
        }

        public void StartAsync()
        {
            Task.Factory.StartNew(() =>
            {
                Start();
            });
        }

        public void Start()
        {
            IsRun = true;
            socket.Listen(20);
            while (IsRun)
            {
                var client = socket.Accept();
                var tcp = new TcpServerSocket(client, this);
                sockets.Add(tcp);
            }
        }
    }
}
