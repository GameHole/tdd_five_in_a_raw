namespace Five
{
    public class OpCodeErrorSerializer : NamedMessageSerializer<OpCodeErrorMessage>
    {
        public override void DeserializeContant(OpCodeErrorMessage msg, ByteStream stream)
        {
            msg.unknownOpcode = stream.Read<int>();
        }

        public override void SerializeContant(OpCodeErrorMessage message, ByteStream stream)
        {
            stream.Write(message.unknownOpcode);
        }
    }
}
