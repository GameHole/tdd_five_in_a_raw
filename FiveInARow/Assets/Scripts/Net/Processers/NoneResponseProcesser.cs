namespace Five
{
    public class NoneResponseProcesser : AProcesser<Response>
    {
        public NoneResponseProcesser(int requestCode)
        {
            OpCode = MessageCode.GetResponseCode(requestCode);
        }
        public override int OpCode { get; }

        public override void ProcessContent(Response message) { }
    }
}
