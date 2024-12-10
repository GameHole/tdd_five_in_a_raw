using Five;
using System.Collections.Generic;

namespace FivesUnitTest
{
    internal class LogProcesser : IProcesser
    {
        internal string log;
        internal object socket;
        internal Message msg;
        internal int _code = 10000;

        public int OpCode => _code;

        public List<Message> msgs = new List<Message>();

        public void Process(AClient socket, Message message)
        {
            log = "Process";
            this.socket = socket;
            msg = message;
            msgs.Add(message);
        }
        public static LogProcesser mockProcesser(Client client,int code=1)
        {
            var log = new LogProcesser();
            client.processer = new MessageProcesser(new OpCodeErrorResponseProcesser());
            client.processer.Processers.Add(code, log);
            return log;
        }
        public static LogProcesser mockServerClient(Client client)
        {
            var log = new LogProcesser();
            var ps = client.processer.Processers;
            ps.Clear();
            ps.Add(1, log);
            return log;
        }
    }
}