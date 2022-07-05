using NUnit.Framework;
using Five;

namespace UnitTests
{
    
    class TestProcesserRegister
    {
        class RegistorInfo
        {
            public int code;
            public System.Type type;
            public RegistorInfo(int code, System.Type type)
            {
                this.code = code;
                this.type = type;
            }
        }
        MessageProcesser processer;
        [SetUp]
        public void SetUp()
        {
            processer = new MessageProcesser(new LogSocket());
            new ProcesserRegister(null, new GameView(), null, null).Regist(processer);
        }
        [Test]
        public void testRegistedNotify()
        {
            RegistorInfo[] notifyInfos = new RegistorInfo[]
            {
                new RegistorInfo(MessageCode.TurnNotify,typeof(TurnProcesser)),
                new RegistorInfo(MessageCode.PlayedNotify,typeof(PlayedProcesser)),
                new RegistorInfo(MessageCode.StartNotify,typeof(StartedProcesser)),
                new RegistorInfo(MessageCode.FinishNotify,typeof(FinishedProcesser))
            };

            foreach (var item in notifyInfos)
            {
                Assert.IsTrue(processer.Processers.TryGetValue(item.code, out var value));
                Assert.AreEqual(item.type, value.GetType());
            }
        }
        [Test]
        public void testRegistedResponse()
        {
            RegistorInfo[] responseInfos = new RegistorInfo[]
            {
                new RegistorInfo(MessageCode.GetResponseCode(MessageCode.RequestMatch),typeof(MatchProcesser)),
                new RegistorInfo(MessageCode.GetResponseCode(MessageCode.RequestCancelMatch),typeof(CancelMatchProcesser)),
                new RegistorInfo(MessageCode.GetResponseCode(MessageCode.RequestPlay),typeof(NoneResponseProcesser)),
            };
            foreach (var item in responseInfos)
            {
                Assert.IsTrue(processer.Processers.TryGetValue(item.code, out var value));
                Assert.AreEqual(value.GetType(), typeof(ResponseDecorater));
                Assert.AreEqual(item.type, (value as ResponseDecorater).decorated.GetType());
            }
        }
    }
}
