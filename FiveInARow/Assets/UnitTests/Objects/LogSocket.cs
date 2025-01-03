﻿using Five;
using System.Net.Sockets;

namespace UnitTests
{
    public class LogSocket : ASocket
    {
        internal string log;

        public override void Close()
        {
            onClose?.Invoke();
        }

        public override void Connect(string ip, int port)
        {
            log = $"Connect {ip}:{port}";
        }

        public override void Send(Message message)
        {
            log = "Send " + message.ToString();
        }
    }
}