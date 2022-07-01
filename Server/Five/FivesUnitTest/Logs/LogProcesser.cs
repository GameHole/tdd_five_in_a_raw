using Five;

namespace FivesUnitTest
{
    internal class LogProcesser : IProcesser
    {
        internal string log;

        public int OpCode => 10000;

        public void Process(Message message)
        {
            log = "Process";
        }
    }
}