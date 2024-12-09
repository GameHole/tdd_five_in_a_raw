using System;

namespace Five
{
    public class PlayerIdNotifySerializer : NamedMessageSerializer<PlayerIdNotify>
    {
        public override void DeserializeContant(PlayerIdNotify msg, ByteStream stream)
        {
            msg.playerId = stream.Read<int>();
        }

        public override void SerializeContant(PlayerIdNotify message, ByteStream stream)
        {
            stream.Write(message.playerId);
        }
    }
}
