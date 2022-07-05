using Five;
using NUnit.Framework;

namespace UnitTests
{
    class TestGameView
    {
        [Test]
        public void testGameView()
        {
            var game = new GameView();

            Assert.AreSame(game.View.transform, game.chessboard.View.transform.parent);
            Assert.AreSame(game.View.transform, game.grading.View.transform.parent);
            Assert.AreSame(game.Canvas, game.countDown.View.transform.parent);
            Assert.AreSame(game.Canvas, game.finishView.View.transform.parent);

            Assert.IsTrue(game.chessboard.View.activeSelf);
            Assert.IsTrue(game.grading.View.activeSelf);
            Assert.IsTrue(game.countDown.View.activeSelf);

            Assert.IsFalse(game.View.activeInHierarchy);
            Assert.IsFalse(game.chessSelector.IsRun);
            Assert.IsFalse(game.finishView.View.activeSelf);

        }
    }
}
