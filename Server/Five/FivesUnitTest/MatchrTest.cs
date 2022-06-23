using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class MatchrTest
    {
        LogMaster master;
        [SetUp]
        public void SetUp()
        {
            master = new LogMaster(new Matching());
        }
        [Test]
        public void testMatch()
        {
            var result = master.Match();
            Assert.AreEqual(ResultDefine.Success, result);
            Assert.AreEqual("Match ", master.log);
            Assert.AreEqual(1, master.GameId);
            result = master.Match();
            Assert.AreEqual(ResultDefine.Matching, result);
            result = master.Cancel();
            Assert.AreEqual(ResultDefine.Success, result);
            result = master.Match();
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testCancelMatch()
        {
            var result = master.Cancel();
            Assert.AreEqual(ResultDefine.NotInMatching, result);
            result = master.Match();
            Assert.AreEqual(ResultDefine.Success, result);
            result = master.Cancel();
            Assert.AreEqual("Match CancelMatch ", master.log);
            Assert.AreEqual(ResultDefine.Success, result);
        }
        [Test]
        public void testMatchOnGameStart()
        {
            master.Started();
            var result = master.Match();
            Assert.AreEqual(ResultDefine.GameStarted, result);
        }
        [Test]
        public void testCancelMatchOnGameStart()
        {
            master.Started();
            var result = master.Cancel();
            Assert.AreEqual(ResultDefine.GameStarted, result);
        }
        [Test]
        public void testPlayerStart()
        {
            master.Player.Start(1);
            Assert.AreEqual("Start ", master.log);
        }
        [Test]
        public void testPlayerFinish()
        {
            master.Player.Finish();
            Assert.AreEqual("Finished ", master.log);
        }
    }
}
