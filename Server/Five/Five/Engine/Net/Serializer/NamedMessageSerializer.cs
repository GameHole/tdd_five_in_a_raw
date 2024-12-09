namespace Five
{
    public abstract class NamedMessageSerializer<T>: ASerializer where T:Message,new()
    {
        public override Message Deserialize(ByteStream stream)
        {
            var item = new T();
            item.opcode = stream.Read<int>();
            DeserializeContant(item, stream);
            return item;
        }
        public override void Serialize(Message message, ByteStream stream)
        {
            stream.Write(message.opcode);
            SerializeContant(message as T, stream);
        }

        public abstract void DeserializeContant(T msg,ByteStream stream);
      
        public abstract void SerializeContant(T message, ByteStream stream);
    }
}
