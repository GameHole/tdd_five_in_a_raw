using Five;
using System.Collections.Generic;

namespace FivesUnitTest
{
    class TReqProc : RequestProcesser
    {
        public override int OpCode => -1;

        public object Client => socket;

        public object Mgr => mgr;

        protected override Response ProcessContant(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
    internal class LogRequestRegister:RequestRegister
    {
        public bool isRun;
        internal object mgr;

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
        public override void Regist(Client client)
        {
            base.Regist(client);
            isRun = true;
            this.client = client;
        }
    }
}