using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class TcpSocket : ASocket
    {
        MessageSerializer serializer = new MessageSerializer();
        public override void Send(Message message)
        {
            
        }

        public void Recv(ByteStream stream)
        {
            onRecv?.Invoke(serializer.Deserialize(stream));
        }
    }
}
