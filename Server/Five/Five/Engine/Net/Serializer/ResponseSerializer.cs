
namespace Five
{
    public class ResponseSerializer : NamedMessageSerializer<Response>
    {
        public override void SerializeContant(Response message, ByteStream stream)
        {
            stream.Write(message.result);
        }

        public override void DeserializeContant(Response msg, ByteStream stream)
        {
            msg.result = stream.Read<int>();
        }
    }
}
