using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public abstract class NamedMessageSerializer<T>: ASerializer where T:Message
    {
        public override Message Deserialize(ByteStream stream)
        {
            stream.Read<int>();
            return DeserializeContant(stream);
        }

        protected abstract Message DeserializeContant(ByteStream stream);

        public override void Serialize(Message message, ByteStream stream)
        {
            stream.Write(message.opcode);
            SerializeContant(message as T, stream);
        }
        public abstract void SerializeContant(T message, ByteStream stream);
    }
}
