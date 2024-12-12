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
            var array = NewProcessers();
            foreach (var item in array)
            {
                p.Processers.Add(item.OpCode, item);
            }
            foreach (var item in array)
            {
                item.Init(servce);
            }
            return p;
        }
        protected virtual RequestProcesser[] NewProcessers()
        {
            return new RequestProcesser[]
            {
                new MatchRequestProcesser(),
                new CancelRequestProcesser(),
                new PlayRequestProcesser()
            };
        }
    }
}
