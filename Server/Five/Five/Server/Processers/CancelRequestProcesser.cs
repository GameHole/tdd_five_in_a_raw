namespace Five
{
    public class CancelRequestProcesser : RequestProcesser
    {
        public override int OpCode => MessageCode.RequestCancelMatch;

        protected override Response ProcessContant(ASocket socket, Message message)
        {
            return new Response().SetInfo(message, app.matchServce.Cancel(socket));
        }
    }
}
