namespace Five
{
    public class CancelRequestProcesser : RequestProcesser
    {
        public override int OpCode => MessageCode.RequestCancelMatch;

        protected override Response ProcessContant(AClient socket, Message message)
        {
            return new Response().SetInfo(message, servce.Cancel(socket));
        }
    }
}
