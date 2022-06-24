using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class ResponseSerializer : NamedMessageSerializer<Response>
    {
        public override Message Deserialize(ByteStream stream)
        {
            return new Response(stream.Read<int>(), stream.Read<int>());
        }
        protected override void SerializeMessage(Response message, ByteStream stream)
        {
            stream.Write(message.result);
        }
    }
}
