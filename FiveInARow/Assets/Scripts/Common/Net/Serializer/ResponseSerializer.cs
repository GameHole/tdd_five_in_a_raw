using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ResponseSerializer : DefaultSerializer
    {
        public override Message Deserialize(ByteStream stream)
        {
            return new Response(stream.Read<int>(), stream.Read<int>());
        }
        public override void Serialize(Message message, ByteStream stream)
        {
            base.Serialize(message, stream);
            stream.Write((message as Response).result);
        }
    }
}
