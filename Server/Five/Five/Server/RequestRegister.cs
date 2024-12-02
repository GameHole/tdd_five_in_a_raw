using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class RequestRegister
    {
        public virtual void Regist(Client client,ClientMgr mgr)
        {
            var array = new RequestProcesser[]
            {
                new MatchRequestProcesser(),
                new CancelRequestProcesser(),
                new PlayRequestProcesser()
            };
            foreach (var item in array)
            {
                item.Init(client.socket, mgr);
                client.processer.Processers.Add(item.OpCode,item);
            }
        }
    }
}
