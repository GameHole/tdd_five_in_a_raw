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
        public ASocket socket { get;private set; }
        public ConcurrentList<AClient> sockets { get; private set; }


        public bool IsRun { get; private set; }
        public MessageProcesser processer { get; }
        private MessageSerializer serializer;

        public Server(ASocket socket, MessageProcesser processer, MessageSerializer serializer)
        {
            this.processer = processer;
            this.socket = socket;
            sockets = new ConcurrentList<AClient>();
            this.serializer = serializer;
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
                var tcp = new DefaultClient(client, serializer);
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
