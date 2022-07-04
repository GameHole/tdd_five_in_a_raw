using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class StartNotifySerializer : NamedMessageSerializer<StartNotify>
    {
        public override void DeserializeContant(StartNotify msg, ByteStream stream)
        {
            int len = stream.Read<int>();
            var infos = new PlayerInfo[len];
            for (int i = 0; i < infos.Length; i++)
            {
                infos[i] = stream.Read<PlayerInfo>();
            }
            msg.infos = infos;
        }

        public override void SerializeContant(StartNotify message, ByteStream stream)
        {
            stream.Write(message.infos.Length);
            for (int i = 0; i < message.infos.Length; i++)
            {
                stream.Write(message.infos[i]);
            }
        }
    }
}
