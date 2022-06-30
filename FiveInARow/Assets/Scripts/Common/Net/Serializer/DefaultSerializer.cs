using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class DefaultSerializer : ASerializer
    {
        public override Message Deserialize(ByteStream stream)
        {
            return new Message(stream.Read<int>());
        }

        public override void Serialize(Message message, ByteStream stream)
        {
            stream.Write(message.opcode);
        }
    }
}
