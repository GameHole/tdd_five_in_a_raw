namespace Five
{
    public class MatchResponseSerializer : NamedResponseSerializer<MatchResponse>
    {
        public override void SerializeContant(MatchResponse message, ByteStream stream)
        {
            base.SerializeContant(message, stream);
            stream.Write(message.playerId);
        }
        public override void DeserializeContant(MatchResponse msg, ByteStream stream)
        {
            base.DeserializeContant(msg, stream);
            msg.playerId = stream.Read<int>();
        }
    }
}
