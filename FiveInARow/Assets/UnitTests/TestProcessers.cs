using Five;
using NUnit.Framework;

namespace UnitTests
{
    class TestProcessers
    {
        [Test]
        public void testStarted()
        {
            var main = new MatchView();
            var game = new GameView();
            game.Close();
            var info = new PlayersInfo();
            var processer = new StartedProcesser(main, game,info);
            var msg = new StartNotify { infos = new PlayerInfo[] { new PlayerInfo(1, 0), new PlayerInfo(2, 1) } };
            processer.Process(msg);
            Assert.IsFalse(main.View.activeInHierarchy);
            Assert.IsTrue(game.View.activeInHierarchy);
        }
        [Test]
        public void testPlayed()
        {
            var chessView = new ChessboardView(new PositionConvertor());
            var players = new PlayersInfo { infos = new PlayerInfo[] { new PlayerInfo(1, 0), new PlayerInfo(2, 1) } };
            var processer = new PlayedProcesser(chessView, players);
            var msg = new PlayedNotify { x = 1, y = 1, id = 1 };
            processer.Process(msg);
            Assert.AreEqual(2,chessView.GetChess(msg.x, msg.y).ChessType);
        }
        [Test]
        public void testFinished()
        {
            var finishView = new FinishView();
            var processer = new FinishedProcesser(finishView, new Player() { id = 1 });
            var msg = new PlayerIdNotify { playerId = 1 };
            processer.Process(msg);
            Assert.IsTrue(finishView.View.activeInHierarchy);
            Assert.IsTrue(finishView.IsWin);
        }
        [Test]
        public void testMatched()
        {
            var player = new Player();
            var match = new MatchView();
            var processer = new MatchProcesser(match, player);
            var msg = new MatchResponse().SetInfo(new Message(MessageCode.RequestMatch),ResultDefine.Success,1);
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
        public void testTurn()
        {
            var countDown = new CountDownView(10);
            var chessSelector = new ChessSelectorView(new TestRay(),new BoardRaycastor(15,15));
            var chessView = countDown.View.GetComponent<CounrDownChessView>();
            var player = new Player() { id = 1 };
            var players = new PlayersInfo() { infos = new PlayerInfo[] { new PlayerInfo(1, 0), new PlayerInfo(2, 1) } };
            var processer = new TurnProcesser(countDown, chessSelector, player, players);
            var msg = new PlayerIdNotify { playerId = 1 };
            processer.Process(msg);
            Assert.AreEqual(chessView.ChessType, 2);
            Assert.AreEqual(countDown.addingTime, 0);
            Assert.IsTrue(chessSelector.IsRun); 
        }
    }
}
