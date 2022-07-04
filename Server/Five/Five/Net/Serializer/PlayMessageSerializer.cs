using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayMessageSerializer:NamedMessageSerializer<PlayMessage>
    {
        public override void SerializeContant(PlayMessage message, ByteStream stream)
        {
            stream.Write(message.x);
            stream.Write(message.y);
        }

        public override void DeserializeContant(PlayMessage msg, ByteStream stream)
        {
            msg.x = stream.Read<int>();
            msg.y = stream.Read<int>();
        }
    }
}
