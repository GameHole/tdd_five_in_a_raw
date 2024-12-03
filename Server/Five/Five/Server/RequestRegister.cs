using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class RequestRegister
    {
        private readonly ClientMgr mgr;

        public RequestRegister(ClientMgr mgr)
        {
            this.mgr = mgr;
        }
        public virtual void Regist(MessageProcesser processer)
        {
            var array = NewArray();
            foreach (var item in array)
            {
                item.Init(mgr);
                processer.Processers.Add(item.OpCode, item);
            }
        }

        protected virtual RequestProcesser[] NewArray()
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
