using Five;
namespace FivesUnitTest
{
    internal class LogRequestRegister:RequestRegister
    {
        public bool isRun;
        internal object mgr;

        public object client;

        public override void Regist(Client client,ClientMgr mgr)
        {
            base.Regist(client, mgr);
            isRun = true;
            this.client = client;
            this.mgr = mgr;
        }
    }
}