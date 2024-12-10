using System;
using System.Collections.Generic;
using System.Text;

namespace Five.RTS
{
    public class RTSProcessFactroy : IProcesserFactroy
    {
        private MatchServce servce;

        public RTSProcessFactroy(MatchServce servce)
        {
            this.servce = servce;
        }

        public MessageProcesser Factroy()
        {
            var p=new MessageProcesser(default);
            var connect= new ConnectProcesser();
            connect.Init(servce);
            var stop = new ServerStopProcesser();
            stop.Init(servce);
            p.connect = connect;
            p.serverStop = stop;
            return p;
        }
    }
}
