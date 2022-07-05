using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class LogClient : MessageProcesser
    {
        internal string log;
        public LogClient(ASocket socket, IProcesser defaultProcesser) : base(socket, defaultProcesser) { }


        public override void Process(Message message)
        {
            base.Process(message);
            log = "Process";
        }
    }
}
