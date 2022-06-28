using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Five
{
    public class TcpSocket : ASocket
    {
        private MessageSerializer serializer = new MessageSerializer();
        private Socket socket;
        public MessagePacker packer { get; private set; }
        public TcpSocket():this(new Socket(SocketType.Stream, ProtocolType.Tcp))
        {
            socket.Bind(new IPEndPoint(IPAddress.Any, 0));
        }

        public TcpSocket(Socket socket)
        {
            this.socket = socket;
            new SerializerRegister().Regist(serializer);
            var proto = new Proto();
            packer = new MessagePacker(proto, 2048);
        }
       

        protected void RecvAsync()
        {
            isVailed = true;
            Task.Factory.StartNew(() =>
            {
                while (isVailed)
                {
                    var stream = packer.stream;
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
                onRecv?.Invoke(serializer.Deserialize(stream));
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
            onClose?.Invoke();
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
