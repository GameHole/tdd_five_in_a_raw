using NUnit.Framework;
using Five;
namespace UnitTests
{
    class TestChessPlacer
    {
        [Test]
        public void testPlacer()
        {
            var selector = new LogChessSelectorView(new TestRay(), new BoardRaycastor(15, 15));
            selector.Start();
            var placer = new ChessPlacer(new TestInput(), selector);
            placer.Update();
            Assert.IsTrue(selector.isPlaced);
        }
    }
}
