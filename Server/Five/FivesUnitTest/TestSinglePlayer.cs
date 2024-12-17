using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    internal class TestSinglePlayer
    {
        private MessageProcesser clientProcesser;
        private ServerProcesser serverProcesser;
        private LocalApp sclient;

        [SetUp]
        public void set()
        {
            serverProcesser = new ServerProcesser(default);

            clientProcesser = new MessageProcesser(default);

            sclient = new LocalApp(clientProcesser,serverProcesser);

        }
        [Test]
        public void testConnect()
        {
            var cLog = new LogProcesser();
            serverProcesser.SetConnectProcesser(cLog);

            var clientLog = new LogProcesser();
            clientProcesser.Add(2, clientLog);

            sclient.Start();
            Assert.NotNull(cLog.client);
            Assert.AreNotSame(sclient, cLog.client);
            Assert.IsNull(cLog.msg);

        }
        [Test]
        public void testMessageProcess()
        {
            var serLog = new LogProcesser();
            serverProcesser.Add(1, serLog);

            var clientLog = new LogProcesser();
            clientProcesser.Add(2, clientLog);

            var msg = new Message(MessageCode.RequestMatch);
            sclient.Send(msg);
            Assert.AreSame(msg, serLog.msg);
            serLog.client.Send(new Message(2));
            Assert.AreEqual(2, clientLog.msg.opcode);
            Assert.AreSame(sclient, clientLog.client);
        }
        [Test]
        public void testClose()
        {
            var cLog = new LogProcesser();
            serverProcesser.serverStop = cLog;
            int i = 0;
            sclient.onClose += ()=>i++;
            sclient.Close();
            Assert.AreEqual("Process", cLog.log);
            Assert.IsNull(cLog.msg);
            Assert.IsNull(cLog.client);
            Assert.AreEqual(1, i);
        }
    }
}
