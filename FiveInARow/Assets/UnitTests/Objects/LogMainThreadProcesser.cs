using Five;
using System.Threading;

namespace UnitTests
{
    internal class LogMainThreadProcesser:IProcesser
    {
        internal int threadId = -1;

        public int OpCode => 10000;

        public void Process(Message message)
        {
            threadId = Thread.CurrentThread.ManagedThreadId;
        }
    }
}