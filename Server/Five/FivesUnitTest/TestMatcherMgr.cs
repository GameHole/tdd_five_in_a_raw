using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FivesUnitTest
{
    class TestMatcherMgr
    {
        MatcherMgr mgr;
        [SetUp]
        public void SetUp()
        {
            mgr = new MatcherMgr();
        }
        [Test]
        public void testClientMgr()
        {
            Assert.AreEqual(0, mgr.matchers.Count);
        }
        [Test]
        public void testonAcceptMgr()
        {
            var socket = new LogSocket();
            mgr.Login(socket);

            Assert.AreEqual(1, mgr.matchers.Count);
            var matcher = mgr.matchers[socket];
            Assert.AreEqual(typeof(NetNotifier), matcher.Player.notifier.GetType());

            var logP = new LogPlayer();
            matcher.Player = logP;
            socket.onClose.Invoke();
            Assert.AreEqual("OutLine ", logP.log);
            Assert.AreEqual(0, mgr.matchers.Count);
        }
       
        [Test]
        public void testSocketClose()
        {
            var scket = new LogSocket();
            mgr.Login(scket);
            scket.Close();
            Assert.AreEqual(0, mgr.matchers.Count);
        }
    }
}
