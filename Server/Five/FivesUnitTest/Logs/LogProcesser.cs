﻿using Five;
using System.Collections.Generic;

namespace FivesUnitTest
{
    internal class LogProcesser : IProcesser
    {
        internal string log;
        internal AClient client;
        internal Message msg;

        public List<Message> msgs = new List<Message>();

        public void Process(AClient client, Message message)
        {
            log = "Process";
            this.client = client;
            msg = message;
            msgs.Add(message);
        }
        public static LogProcesser mockProcesser(Client client,int code=1)
        {
            var log = new LogProcesser();
            client.processer = new MessageProcesser(new OpCodeErrorResponseProcesser());
            client.processer.Add(code, log);
            return log;
        }
        public static LogProcesser mockServerClient(Client client)
        {
            var log = new LogProcesser();
            var ps = client.processer;
            ps.Clear();
            ps.Add(1, log);
            return log;
        }
    }
}