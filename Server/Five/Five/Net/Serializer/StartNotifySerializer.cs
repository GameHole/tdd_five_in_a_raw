using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class StartNotifySerializer : NamedMessageSerializer<StartNotify>
    {
        public override void SerializeContant(StartNotify message, ByteStream stream)
        {
            stream.Write(message.infos.Length);
            for (int i = 0; i < message.infos.Length; i++)
            {
                stream.Write(message.infos[i]);
            }
        }

        protected override Message DeserializeContant(ByteStream stream)
        {
            int len = stream.Read<int>();
            var info = new PlayerInfo[len];
            for (int i = 0; i < info.Length; i++)
            {
                info[i]= stream.Read<PlayerInfo>();
            }
            return new StartNotify(info);
        }
    }
}
