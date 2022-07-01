using Five;

namespace UnitTests
{
    internal class LogProcesserRegister : IProcesserRegister
    {
        internal string log;

        public void Regist(MessageProcesser client)
        {
            log = "registed";
        }
    }
}