using System;

namespace Five
{
    public class FinishNotifySerializer : NamedMessageSerializer<FinishNotify>
    {
        public override void DeserializeContant(FinishNotify msg, ByteStream stream)
        {
            msg.id = stream.Read<int>();
        }

        public override void SerializeContant(FinishNotify message, ByteStream stream)
        {
            stream.Write(message.id);
        }
    }
}
