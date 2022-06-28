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

        protected override Message DeserializeContant(ByteStream stream)
        {
            return new PlayRequest(stream.Read<int>(), stream.Read<int>());
        }
    }
}
