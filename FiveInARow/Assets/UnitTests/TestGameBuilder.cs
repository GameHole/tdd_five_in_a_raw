using Five;
using NUnit.Framework;
using UnityEngine;

namespace UnitTests
{
    class TestGameBuilder
    {
        [Test]
        public void testBuilder()
        {
            var cntr = new Container();
            new GameBuilder(15,15,Camera.main).Build(cntr);
            Assert.NotNull(cntr.Get<ChessboardView>());
            Assert.NotNull(cntr.Get<GradingView>());
            Assert.NotNull(cntr.Get<CountDownView>());
            Assert.NotNull(cntr.Get<ChessSelectorView>());
            Assert.NotNull(cntr.Get<FinishView>());
            Assert.NotNull(cntr.Get<SelfChessView>());
        }
    }
}
