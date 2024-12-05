using Five;

namespace FivesUnitTest
{
    internal class LogProcesser : IProcesser
    {
        internal string log;
        internal object socket;
        internal object msg;

        public int OpCode => 10000;

        public void Process(AClient socket, Message message)
        {
            log = "Process";
            this.socket = socket;
            msg = message;
        }
    }
}