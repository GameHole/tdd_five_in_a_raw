using Five;
using NUnit.Framework;
using UnityEngine;

namespace UnitTests
{
    class TestGameView
    {
        Container container;
        GameView game;
        ChessboardView chessboard;
        GradingView grading;
        CountDownView countDown;
        FinishView finishView;
        SelfChessView self;
        [SetUp]
        public void SetUp()
        {
            container = new Container();
            new GameBuilder(15,15,Camera.main).Build(container);
            game = new GameView(container);
            chessboard = container.Get<ChessboardView>();
            grading = container.Get<GradingView>();
            countDown= container.Get<CountDownView>();
            finishView = container.Get<FinishView>();
            self = container.Get<SelfChessView>();
        }
        [Test]
        public void testGameView()
        {
            Assert.NotNull(game.Canvas);
            Assert.NotNull(container.Get<GameView>());
            Assert.IsFalse(chessboard.View.activeInHierarchy);
            Assert.IsFalse(grading.View.activeInHierarchy);
            Assert.IsFalse(countDown.View.activeInHierarchy);
            Assert.IsFalse(game.View.activeInHierarchy);
            Assert.IsFalse(container.Get<ChessSelectorView>().IsRun);
            Assert.IsFalse(finishView.View.activeInHierarchy);
            Assert.IsFalse(self.View.activeInHierarchy);
        }
        [Test]
        public void testOpen()
        {
            game.Open();
            Assert.IsTrue(chessboard.View.activeInHierarchy);
            Assert.IsTrue(grading.View.activeInHierarchy);
            Assert.IsTrue(countDown.View.activeInHierarchy);
            Assert.IsTrue(game.View.activeInHierarchy);
            finishView.Open();
            Assert.IsTrue(finishView.View.activeInHierarchy);
            Assert.IsTrue(self.View.activeInHierarchy);
        }
        [Test]
        public void testClose()
        {
            game.Close();
            Assert.IsFalse(chessboard.View.activeInHierarchy);
            Assert.IsFalse(grading.View.activeInHierarchy);
            Assert.IsFalse(countDown.View.activeInHierarchy);
            Assert.IsFalse(game.View.activeInHierarchy);
            finishView.Open();
            Assert.IsFalse(finishView.View.activeInHierarchy);
            Assert.IsFalse(self.View.activeInHierarchy);
        }
    }
}
