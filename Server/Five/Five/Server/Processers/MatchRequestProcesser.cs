namespace Five
{
    public class MatchRequestProcesser : RequestProcesser
    {
        public override int OpCode => MessageCode.RequestMatch;

        protected override Response ProcessContant(Message message)
        {
            return new MatchResponse().SetInfo(message, matcher.Match(), matcher.Player.PlayerId);
        }
    }
}
