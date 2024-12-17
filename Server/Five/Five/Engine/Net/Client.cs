using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Five
{
    public class Client : AClient
    {
        public bool isVailed { get; protected set; }
        public ASocket socket { get; }
        public MessagePacker packer { get; private set; }
        public MessageProcesser processer { get; set; }
        protected MessageSerializer serializer;

        public Client(ASocket socket, MessageSerializer serializer)
        {
            this.socket = socket;
            this.serializer = serializer;
          
            var proto = new Proto();
            packer = new MessagePacker(proto, 2048);
        }
        public void RecvAsync()
        {
            isVailed = true;
            Task.Factory.StartNew(() =>
            {
                while (isVailed)
                {
                    var stream = packer.recevingStream;
                    int count = socket.Receive(stream.Bytes, stream.Count, stream.GetLastCapacity(),SocketFlags.None);
                    stream.Count += count;
                    if (count == 0)
                    {
                        CloseInternal();
                    }
                    else
                    {
                        Recv();
                    }
                }
            });
        }

        internal void Release()
        {
            isVailed = false;
            socket.Close();
        }

        public override void Send(Message message)
        {
            ByteStream stream = new ByteStream();
            serializer.Serialize(message, stream);
            stream = packer.PackNew(stream);
            socket.Send(stream.Bytes, 0, stream.Count,SocketFlags.None);
        }

        public void Recv()
        {
            while (packer.Unpack(out ByteStream stream))
            {
                var msg = serializer.Deserialize(stream);
                processer?.Process(this, msg);
            }
            packer.MoveBrokenBytesToHead();
        }

        public void Connect(string ip, int port)
        {
            socket.Connect(ip, port);
            RecvAsync();
        }
        protected virtual void CloseInternal()
        {
            Release();
            base.Close();
        }
        public override void Close()
        {
            if (isVailed)
            {
                socket.Disconnect(false);
            }
            CloseInternal();
        }
    }
}
