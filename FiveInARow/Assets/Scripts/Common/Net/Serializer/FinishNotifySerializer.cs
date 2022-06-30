using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class FinishNotifySerializer : NamedMessageSerializer<FinishNotify>
    {
        public override void SerializeContant(FinishNotify message, ByteStream stream)
        {
            stream.Write(message.id);
        }

        protected override Message DeserializeContant(ByteStream stream)
        {
            return new FinishNotify(stream.Read<int>());
        }
    }
}
