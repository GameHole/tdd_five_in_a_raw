using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class PlayMessageSerializer:NamedMessageSerializer<PlayMessage>
    {
        public override Message Deserialize(ByteStream stream)
        {
            stream.Read<int>();
            return new PlayMessage(stream.Read<int>(), stream.Read<int>());
        }

        protected override void SerializeMessage(PlayMessage message, ByteStream stream)
        {
            stream.Write(message.x);
            stream.Write(message.y);
        }
    }
}
