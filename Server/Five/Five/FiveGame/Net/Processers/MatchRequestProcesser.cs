namespace Five
{
    public class MatchRequestProcesser : RequestProcesser
    {
        public override int OpCode => MessageCode.RequestMatch;

        protected override Response ProcessContant(AClient client, Message message)
        {
            return new Response().SetInfo(message, servce.Match(client.Id));
        }
    }
}
