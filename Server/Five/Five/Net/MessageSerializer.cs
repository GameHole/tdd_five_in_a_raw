using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class MessageSerializer: MessageContainer<ASerializer>
    {
        public void Serialize(Message message,ByteStream stream)
        {
            container[message.opcode].Serialize(message, stream);
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
            container.TryGetValue(code, out var serializer);
            return serializer;
        }

        public bool TryDeserialize(ByteStream stream, out Message message)
        {
            message = default;
            int opcode = stream.Peek<int>();
            if (container.TryGetValue(opcode,out var serializer))
            {
                message = serializer.Deserialize(stream);
                return true;
            }
            return false;
        }
    }
}
