using Five;

namespace FivesUnitTest
{
    internal class LogProcesser : IProcesser
    {
        internal string log;
        internal object socket;
        internal object msg;

        public int OpCode => 10000;

        public void Process(ASocket socket, Message message)
        {
            log = "Process";
            this.socket = socket;
            msg = message;
        }
    }
}