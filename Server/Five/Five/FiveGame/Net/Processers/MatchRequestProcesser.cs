namespace Five
{
    public class MatchRequestProcesser : RequestProcesser
    {
        public override int OpCode => MessageCode.RequestMatch;

        protected override Response ProcessContant(AClient socket, Message message)
        {
            return new Response().SetInfo(message, servce.Match(socket));
        }
    }
}
