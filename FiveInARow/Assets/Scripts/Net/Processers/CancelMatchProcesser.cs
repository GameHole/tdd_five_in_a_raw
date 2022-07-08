namespace Five
{
    public class CancelMatchProcesser : AProcesser<Response>
    {
        private MatchView match;

        public CancelMatchProcesser(MatchView match)
        {
            this.match = match;
        }

        public override int OpCode => MessageCode.GetResponseCode(MessageCode.RequestCancelMatch);

        public override void ProcessContent(Response message)
        {
            match.Canceled();
        }
    }
}
