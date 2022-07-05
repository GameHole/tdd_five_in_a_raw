using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    class TestClient
    {
        ProcesserRegister register;
        [SetUp]
        public void SetUp()
        {
            register = new ProcesserRegister(new MatchView(),new GameView(),new Player(),new PlayersInfo());
        }
        [Test]
        public void testConnect()
        {
            var socket = new LogSocket();
            var client = new Client(socket, register);
            var result = client.ConnectAsync("1.1.1.1", 0).GetAwaiter();
            Assert.AreEqual(ResultDefine.Success, result.GetResult());
            Assert.AreEqual("Connect 1.1.1.1:0", socket.log);
        }
        [Test]
        public void testConnectError()
        {
            var socket = new ErrorSocket();
            var client = new Client(socket, register);
            var result = client.ConnectAsync("1.1.1.1", 0).GetAwaiter();
            Assert.AreEqual(LocalResultDefine.ConnectError, result.GetResult());
        }
        [Test]
        public void testRunResister()
        {
            var log = new LogProcesserRegister();
            var client = new Client(new LogSocket(), log);
            Assert.AreEqual("registed",log.log);
        }
    }
}
