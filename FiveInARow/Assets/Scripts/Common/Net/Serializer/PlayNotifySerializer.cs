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

        public override void DeserializeContant(PlayedNotify msg, ByteStream stream)
        {
            play.DeserializeContant(msg, stream);
            msg.id = stream.Read<int>();
        }
    }
}
