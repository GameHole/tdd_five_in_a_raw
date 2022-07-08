using Five;
using NUnit.Framework;

namespace UnitTests
{
    class TestProcessers
    {
        GameFlow flow;
        LogFlow log;
        PlayersInfo players;
        [SetUp]
        public void SetUp()
        {
            flow = new GameFlow();
            log = new LogFlow();
            flow.AddFlow(log);
            players = new PlayersInfo { infos = new PlayerInfo[] { new PlayerInfo(1, 0), new PlayerInfo(2, 1) } };
        }
        [Test]
        public void testStarted()
        {
            var player = new Player();
            var processer = new StartedProcesser(player);
            processer.SetInfos(flow, players);
            var msg = new StartNotify { playerId = 1, infos = new PlayerInfo[] { new PlayerInfo(1, 0), new PlayerInfo(2, 1) } };
            processer.Process(msg);
            Assert.AreEqual(2, player.chess);
            Assert.AreEqual("Start chess:2 ", log.log);
        }
        [Test]
        public void testPlayed()
        {
            var processer = new PlayedProcesser();
            processer.SetInfos(flow, players);
            var msg = new PlayedNotify { x = 1, y = 1, id = 1 };
            processer.Process(msg);
            Assert.AreEqual("Play chess:2 Pos:(1,1) ", log.log);
        }
        [Test]
        public void testTurn()
        {
            var processer = new TurnProcesser();
            processer.SetInfos(flow, players);
            var msg = new PlayerIdNotify { playerId = 1 };
            processer.Process(msg);
            Assert.AreEqual("Turn chess:2 ", log.log);
        }
        [Test]
        public void testFinished()
        {
            var processer = new FinishedProcesser();
            processer.SetInfos(flow, players);
            var msg = new PlayerIdNotify { playerId = 1 };
            processer.Process(msg);
            Assert.AreEqual("Finish chess:2 ", log.log);
        }
        [Test]
        public void testMatched()
        {
            var match = new MatchView();
            var processer = new MatchProcesser(match);
            var msg = new Response().SetInfo(new Message(MessageCode.RequestMatch), ResultDefine.Success);
            processer.Process(msg);
            Assert.IsTrue(match.CancelView.activeSelf);
            Assert.IsFalse(match.MatchingView.activeSelf);
        }
        [Test]
        public void testCancelMatched()
        {
            var match = new MatchView();
            var processer = new CancelMatchProcesser(match);
            var msg = new Response().SetInfo(new Message(MessageCode.RequestCancelMatch), ResultDefine.Success);
            processer.Process(msg);
            Assert.IsFalse(match.CancelView.activeSelf);
            Assert.IsTrue(match.MatchingView.activeSelf);
        }
     
        [Test]
        public void testOpCodeError()
        {
            var processer = new OpCodeNotFoundProcesser();
            var loger = new LogLoger();
            processer.logger = loger;
            processer.Process(new Message(1));
            Assert.AreEqual("OpCode:1 not process", loger.logStr);
        }
    }
}
