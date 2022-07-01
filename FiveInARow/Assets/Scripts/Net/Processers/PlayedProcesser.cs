namespace Five
{
    public class PlayedProcesser : IProcesser
    {
        public int OpCode => MessageCode.PlayedNotify;

        public void Process(Message message)
        {
            
        }
    }
}
