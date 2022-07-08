using NUnit.Framework;
using Five;
namespace UnitTests
{
    class TestFlows
    {
        [Test]
        public void testStartView()
        {
            var main = new MatchView();
            var game = new GameView();
            game.Close();
            var start = new StartViewController() { main = main, game = game };
            start.Start(1);
            Assert.IsFalse(main.View.activeInHierarchy);
            Assert.IsTrue(game.View.activeInHierarchy);
        }
        [Test]
        public void testChessSetter()
        {
            var view = new SelfChessView();
            var start = new SelfChessSetter() { self = view };
            start.Start(2);
            Assert.AreEqual(2, view.View.GetComponent<AChessView>().ChessType);
        }
        [Test]
        public void testChessboardViewController()
        {
            var view = new ChessboardView(new PositionConvertor());
            var start = new ChessboardViewController() { chessView = view };
            start.Play(2, new UnityEngine.Vector2Int(1, 1));
            Assert.AreEqual(2, view.GetChess(1, 1).ChessType);
            start.Start(0);
            Assert.AreEqual(0, view.Count);
        }
        [Test]
        public void testFinishViewController()
        {
            var finishView = new FinishView();
            var finish = new FinishViewController() { finishView = finishView, player = new Player { chess = 1 } };
            finish.Finish(1);
            Assert.IsTrue(finishView.View.activeInHierarchy);
            Assert.IsTrue(finishView.IsWin);
        }
        [Test]
        public void testCountDownViewController()
        {
            var countView = new CountDownView(10);
            var finish = new CountDownViewController() { countView = countView };
            finish.Finish(1);
            Assert.IsFalse(countView.View.activeInHierarchy);
            finish.Start(1);
            Assert.IsTrue(countView.View.activeInHierarchy);
            countView.Update(5);
            finish.Turn(1);
            Assert.AreEqual(0, countView.addingTime);
        }
        [Test]
        public void testChessSelectorController()
        {
            var selector = new ChessSelectorView(new TestRay(), new BoardRaycastor(10, 10));
            selector.Start();
            var finish = new ChessSelectorController() { selector = selector };
            finish.Finish(1);
            Assert.IsFalse(selector.IsRun);
        }
        [Test]
        public void testChessSelectorController_When_Turn()
        {
            var selector = new ChessSelectorView(new TestRay(), new BoardRaycastor(10, 10));
            var finish = new ChessSelectorController() { selector = selector, player = new Player { chess = 2 } };
            finish.Turn(2);
            Assert.IsTrue(selector.IsRun);
            finish.Turn(1);
            Assert.IsFalse(selector.IsRun);
        }
        [Test]
        public void testClessTypeSetter()
        {
            var chessSelector = new ChessSelectorView(new TestRay(), new BoardRaycastor(15, 15));
            var countDown = new CountDownView(10);
            var setter = new TurnChessTypeSetter().Add(countDown.View).Add(chessSelector.ChessPreview);
            setter.Turn(2);
            Assert.AreEqual(2, countDown.View.GetComponent<CounrDownChessView>().ChessType);
            Assert.AreEqual(2, chessSelector.ChessPreview.GetComponent<AChessView>().ChessType);
        }
    }
}
