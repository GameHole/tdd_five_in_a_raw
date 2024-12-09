namespace Five
{
    public class PlayRequest : PlayMessage
    {
        public PlayRequest()
        {
            opcode = MessageCode.RequestPlay;
        }
    }
}
