using Five;
namespace FivesUnitTest
{
    internal class LogRequestRegister:RequestRegister
    {
        public bool isRun;
        public override void Regist(Client client)
        {
            base.Regist(client);
            isRun = true;
        }
    }
}