using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class PlayNotifySerializer : NamedMessageSerializer<PlayedNotify>
    {
        private PlayMessageSerializer play = new PlayMessageSerializer();
        public override void SerializeContant(PlayedNotify message, ByteStream stream)
        {
            play.SerializeContant(message, stream);
            stream.Write(message.id);
        }

        protected override Message DeserializeContant(ByteStream stream)
        {
            return new PlayedNotify(stream.Read<int>(), stream.Read<int>(), stream.Read<int>());
        }
    }
}
