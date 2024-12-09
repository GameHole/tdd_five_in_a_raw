namespace Five
{
    public class NamedResponseSerializer<T> : NamedMessageSerializer<T> where T : Response,new()
    {
        public override void SerializeContant(T message, ByteStream stream)
        {
            stream.Write(message.result);
        }

        public override void DeserializeContant(T msg, ByteStream stream)
        {
            msg.result = stream.Read<int>();
        }
    }
}
