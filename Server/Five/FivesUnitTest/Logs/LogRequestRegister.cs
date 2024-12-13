using Five;
using System.Collections.Generic;

namespace FivesUnitTest
{
    class TReqProc : RequestProcesser
    {
        internal object msgSock;
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
        internal App processer;

        public LogRequestRegister(MatchServce mgr) : base(mgr)
        {
        }
        protected override Binder[] NewProcessers()
        {
            List<Binder> list = new List<Binder>(base.NewProcessers());
            list.Add(new Binder(-1, test));
            return list.ToArray();
        }
        public override App Factroy()
        {
            var t = base.Factroy();
            this.processer = t;
            return t;
        }
    }
}