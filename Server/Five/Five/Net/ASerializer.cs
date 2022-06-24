namespace Five
{
    abstract class ASerializer
    {
        public abstract void Serialize(Message message, ByteStream stream);
        public abstract Message Deserialize(ByteStream stream);
    }
}