using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class MessageSerializer
    {
        public MessageContainer<ASerializer> Container{ get; private set; } = new MessageContainer<ASerializer>();
        private DefaultSerializer defaultSerializer = new DefaultSerializer();
        public void Serialize(Message message, ByteStream stream)
        {
            if (Container.TryGetValue(message.opcode, out var serializer))
                serializer.Serialize(message, stream);
        }
        public Message Deserialize(ByteStream stream)
        {
            int opcode = stream.Peek<int>();
            if (Container.TryGetValue(opcode, out var serializer))
            {
                return serializer.Deserialize(stream);
            }
            else
            {
                return defaultSerializer.Deserialize(stream);
            }
        }

        public ASerializer GetSerializer(int code)
        {
            Container.TryGetValue(code, out var serializer);
            return serializer;
        }
    }
}
