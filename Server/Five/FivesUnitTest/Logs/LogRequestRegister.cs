﻿using Five;
using System.Collections.Generic;

namespace FivesUnitTest
{
    class TReqProc : RequestProcesser
    {
        internal object msgSock;

        public override int OpCode => -1;


        public object Mgr => servce;

        protected override Response ProcessContant(AClient socket, Message message)
        {
            msgSock = socket;
            return new Response();
        }
    }
    internal class LogRequestRegister:ProcesserFactroy
    {

        public TReqProc test = new TReqProc();
        internal object processer;

        public LogRequestRegister(MatchServce mgr) : base(mgr)
        {
        }
        protected override RequestProcesser[] NewProcessers()
        {
            List<RequestProcesser> list = new List<RequestProcesser>(base.NewProcessers());
            list.Add(test);
            return list.ToArray();
        }
        public override MessageProcesser Factroy()
        {
            var t = base.Factroy();
            this.processer = t;
            return t;
        }
    }
}