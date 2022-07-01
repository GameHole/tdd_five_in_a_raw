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
        [Test]
        public void testRegisted()
        {
            RegistorInfo[] infos = new RegistorInfo[]
            {
                new RegistorInfo(MessageCode.PlayedNotify,typeof(PlayedProcesser))
            };
            var processer = new MessageProcesser(new LogSocket());
            new ProcesserRegister().Regist(processer);
            foreach (var item in infos)
            {
                Assert.IsTrue(processer.Processers.TryGetValue(item.code,out var value));
                Assert.AreEqual(value.GetType(),item.type);
            }
        }
    }
}
