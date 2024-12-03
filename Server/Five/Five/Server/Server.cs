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
        public ConcurrentList<TcpSocket> sockets { get; private set; }
        public bool IsRun { get; private set; }
        public App app { get; }
        public MessageProcesser processer { get; }

        public Server(string ip,int port,ProcesserFactroy factroy)
        {
            processer = factroy.Factroy();
            socket = new Socket(SocketType.Stream,ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            sockets = new ConcurrentList<TcpSocket>();
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
            processer.serverStop.Process(default,default);
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
                var tcp = new TcpSocket(client,new SerializerRegister());
                tcp.RecvAsync();
                tcp.onClose += () => sockets.Remove(tcp);
                sockets.Add(tcp);
                processer.connect.Process(tcp, default);
                tcp.onRecv = (message) =>
                {
                    processer.Process(tcp, message);
                };
            }
        }
    }
}
