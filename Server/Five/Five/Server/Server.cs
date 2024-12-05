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
        public ANetSocket socket { get;private set; }
        public ConcurrentList<ASocket> sockets { get; private set; }
        public bool IsRun { get; private set; }
        public MessageProcesser processer { get; }
        private SerializerRegister serializer = new SerializerRegister();
        public Server(ANetSocket socket,ProcesserFactroy factroy)
        {
            processer = factroy.Factroy();
            this.socket = socket;
            sockets = new ConcurrentList<ASocket>();
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
                var tcp = new TcpSocket(client, serializer);
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
