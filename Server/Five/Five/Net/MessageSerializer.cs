using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class MessageSerializer
    {
        Dictionary<int, ASerializer> serizers = new Dictionary<int, ASerializer>();
        public MessageSerializer()
        {
            Add(MessageCode.RequestMatch, new DefaultSerializer());
            Add(MessageCode.GetResponseCode(MessageCode.RequestMatch), new ResponseSerializer());
            Add(MessageCode.RequestPlay, new PlayMessageSerializer());
        }
        void Add(int code,ASerializer serializer)
        {
            serizers.Add(code, serializer);
        }
        public void Serialize(Message message,ByteStream stream)
        {
            serizers[message.opcode].Serialize(message, stream);
        }
        public Message Deserialize(ByteStream stream)
        {
            int opcode = stream.Peek<int>();
            return serizers[opcode].Deserialize(stream);
        }
    }
}
