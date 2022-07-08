namespace Five
{
    public class MatchProcesser : AProcesser<Response>
    {
        private MatchView match;

        public MatchProcesser(MatchView match)
        {
            this.match = match;
        }

        public override int OpCode => MessageCode.GetResponseCode(MessageCode.RequestMatch);

        public override void ProcessContent(Response message)
        {
            match.Matched();
        }
    }
}
