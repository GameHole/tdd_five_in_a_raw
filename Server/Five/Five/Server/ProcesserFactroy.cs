using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ProcesserFactroy
    {
        private readonly App app;

        public ProcesserFactroy(App mgr)
        {
            this.app = mgr;
        }
        public virtual MessageProcesser Factroy()
        {
            MessageProcesser processer = new MessageProcesser(new OpCodeErrorResponseProcesser());
            processer.connect = new ConnectProcesser(app);
            processer.serverStop = new ServerStopProcesser(app);
            var array = NewProcessers();
            foreach (var item in array)
            {
               processer.Processers.Add(item.OpCode, item);
            }
            foreach (var item in array)
            {
                item.Init(app.mgr);
            }
            return processer;
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
