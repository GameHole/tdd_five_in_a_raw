using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    abstract class NamedMessageSerializer<T>:DefaultSerializer where T:Message
    {
        public override void Serialize(Message message, ByteStream stream)
        {
            base.Serialize(message, stream);
            SerializeMessage(message as T, stream);
        }
        protected abstract void SerializeMessage(T message, ByteStream stream);
    }
}
