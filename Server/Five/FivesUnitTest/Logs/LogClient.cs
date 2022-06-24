using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class LogClient : Client
    {
        internal string log;

        public LogClient(ASocket socket, Matcher matcher) : base(socket, matcher)
        {
        }
        public override void Process(Message message)
        {
            base.Process(message);
            log = "Process";
        }
    }
}
