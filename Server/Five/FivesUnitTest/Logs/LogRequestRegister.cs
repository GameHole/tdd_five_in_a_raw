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
    internal class LogRequestRegister:RequestRegister
    {
        public bool isRun;

        public object client;
        public TReqProc test = new TReqProc();
        public LogRequestRegister(ClientMgr mgr) : base(mgr)
        {
        }
        protected override RequestProcesser[] NewArray()
        {
            List<RequestProcesser> list = new List<RequestProcesser>(base.NewArray());
            list.Add(test);
            return list.ToArray();
        }
        public override void Regist(MessageProcesser processer)
        {
            base.Regist(processer);
            isRun = true;
            this.client = processer;
        }
    }
}