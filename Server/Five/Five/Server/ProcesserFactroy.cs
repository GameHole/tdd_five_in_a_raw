using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ProcesserFactroy
    {
        private readonly ClientMgr mgr;

        public ProcesserFactroy(ClientMgr mgr)
        {
            this.mgr = mgr;
        }
        public virtual MessageProcesser Factroy()
        {
            MessageProcesser processer = new MessageProcesser(new OpCodeErrorResponseProcesser());
            var array = NewProcessers();
            foreach (var item in array)
            {
               processer.Processers.Add(item.OpCode, item);
            }
            foreach (var item in array)
            {
                item.Init(mgr);
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
