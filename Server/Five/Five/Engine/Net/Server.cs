﻿using System;
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
        public ConcurrentList<Client> clients { get; private set; }


        public bool IsRun { get; private set; }
        public MessageProcesser processer { get; }
        private MessageSerializer serializer;

        public Server(ASocket socket, MessageProcesser processer, MessageSerializer serializer)
        {
            this.processer = processer;
            this.socket = socket;
            clients = new ConcurrentList<Client>();
            this.serializer = serializer;
        }
        public virtual void Stop()
        {
            foreach (var item in clients)
            {
                item.Release();
            }
            clients.Clear();
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
                var accepted = socket.Accept();
                var client = new Client(accepted, serializer);
                client.RecvAsync();
                client.onClose += () => clients.Remove(client);
                clients.Add(client);
                processer.connect.Process(client, default);
                client.processer = processer;
                //client.onRecv = (message) =>
                //{
                //    processer.Process(client, message);
                //};
            }
        }
    }
}
