using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Five;
namespace FivesUnitTest
{
    class TestSerializerRegister
    {
        MessageSerializer ser;
        [SetUp]
        public void SetUp()
        {
            ser = new MessageSerializer();
            new SerializerRegister().Regist(ser);
        }
        [Test]
        public void testMessafeSerializerRegister()
        {
            int[] codes = new int[]
            {
                MessageCode.RequestMatch,
                MessageCode.RequestPlay,
                MessageCode.RequestCancelMatch,
                MessageCode.StartNotify,
                MessageCode.PlayedNotify,
                MessageCode.FinishNotify
            };
            Type[] requestTypes = new Type[]
            {
                typeof(DefaultSerializer),
                typeof(PlayMessageSerializer),
                typeof(DefaultSerializer),
                typeof(StartNotifySerializer),
                typeof(PlayNotifySerializer),
                typeof(FinishNotifySerializer),
            };
            for (int i = 0; i < codes.Length; i++)
            {
                int code = codes[i];
                Assert.IsTrue(ser.Container.Contains(code), code.ToString());
                Assert.AreEqual(requestTypes[i], ser.GetSerializer(code).GetType());
            }

        }
        [Test]
        public void testResponseSerializerRegister()
        {
            int[] codes = new int[]
            {
                MessageCode.RequestMatch,
                MessageCode.RequestPlay,
                MessageCode.RequestCancelMatch
            };
            Type[] responseTypes = new Type[]
            {
                typeof(MatchResponseSerializer),
                typeof(ResponseSerializer),
                typeof(ResponseSerializer)
            };
            for (int i = 0; i < codes.Length; i++)
            {
                int res = MessageCode.GetResponseCode(codes[i]);
                Assert.IsTrue(ser.Container.Contains(res), res.ToString());
                Assert.AreEqual(responseTypes[i], ser.GetSerializer(res).GetType());
            }
        }
    }
}
