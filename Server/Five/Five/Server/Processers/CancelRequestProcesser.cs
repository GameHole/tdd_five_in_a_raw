namespace Five
{
    public class CancelRequestProcesser : RequestProcesser
    {
        public override int OpCode => MessageCode.RequestCancelMatch;

        protected override Response ProcessContant(Message message)
        {
            return new Response().SetInfo(message, mgr.Cancel(socket));
        }
    }
}
