namespace Five
{
    public class MatchRequestProcesser : RequestProcesser
    {
        protected override Response ProcessContant(AClient client, Message message)
        {
            return new Response().SetInfo(message, domain.matchServce.Match(client.Id));
        }
    }
}
