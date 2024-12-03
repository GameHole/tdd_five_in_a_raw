namespace Five
{
    public class MatchRequestProcesser : RequestProcesser
    {
        public override int OpCode => MessageCode.RequestMatch;

        protected override Response ProcessContant(ASocket socket, Message message)
        {
            return new Response().SetInfo(message, app.matchServce.Match(socket));
        }
    }
}
