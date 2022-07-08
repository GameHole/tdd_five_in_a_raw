using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class RequestRegister
    {
        public virtual void Regist(Client client)
        {
            var array = new RequestProcesser[]
            {
                new MatchRequestProcesser(),
                new CancelRequestProcesser(),
                new PlayRequestProcesser()
            };
            foreach (var item in array)
            {
                item.Init(client.socket, client.matcher);
                client.processer.Processers.Add(item.OpCode,item);
            }
        }
    }
}
