using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class MessageSerializer
    {
        public MessageContainer<ASerializer> Container{ get; private set; } = new MessageContainer<ASerializer>();
        public void Serialize(Message message, ByteStream stream)
        {
            if (Container.TryGetValue(message.opcode, out var serializer))
                serializer.Serialize(message, stream);
        }
        public Message Deserialize(ByteStream stream)
        {
            if (!TryDeserialize(stream,out var message))
            {
                throw new KeyNotFoundException($"OpCode = {stream.Peek<int>()}");
            }
            return message;
        }

        public ASerializer GetSerializer(int code)
        {
            Container.TryGetValue(code, out var serializer);
            return serializer;
        }

        public bool TryDeserialize(ByteStream stream, out Message message)
        {
            message = default;
            int opcode = stream.Peek<int>();
            if (Container.TryGetValue(opcode,out var serializer))
            {
                message = serializer.Deserialize(stream);
                return true;
            }
            return false;
        }
    }
}
