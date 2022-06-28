using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class RequestRegister
    {
        ASocket socket;
        Matcher matcher;
        public RequestRegister(ASocket socket, Matcher matcher)
        {
            this.socket = socket;
            this.matcher = matcher;
        }
        public void Regist(Client client)
        {
            var array = new RequestProcesser[]
            {
                new MatchRequestProcesser(),
                new CancelRequestProcesser(),
                new PlayRequestProcesser()
            };
            foreach (var item in array)
            {
                item.Init(socket, matcher);
                client.Add(item.MessageCode,item);
            }
        }
    }
}
