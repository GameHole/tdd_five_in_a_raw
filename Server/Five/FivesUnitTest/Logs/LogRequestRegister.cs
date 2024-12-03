using Five;
using System.Collections.Generic;

namespace FivesUnitTest
{
    class TReqProc : RequestProcesser
    {
        internal object msgSock;

        public override int OpCode => -1;

        public object Client;

        public object Mgr => mgr;

        protected override Response ProcessContant(ASocket socket, Message message)
        {
            msgSock = socket;
            return new Response();
        }
    }
    internal class LogRequestRegister:ProcesserFactroy
    {

        public TReqProc test = new TReqProc();
        internal object processer;

        public LogRequestRegister(App mgr) : base(mgr)
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