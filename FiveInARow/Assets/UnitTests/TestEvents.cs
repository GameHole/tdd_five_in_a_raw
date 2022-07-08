using Five;
using NUnit.Framework;

namespace UnitTests
{
    class TestEvents
    {
        [Test]
        public void testMatchEvents()
        {
            var socket = new LogSocket();
            var match = new MatchView();
            var sender = new MatchButtonEvents(match, socket);
            match.matchBtn.Invoke();
            Assert.AreEqual("Send opcode:1", socket.log);
            match.cancelBtn.Invoke();
            Assert.AreEqual("Send opcode:3", socket.log);
        }
        [Test]
        public void testFinishEvents()
        {
            var finish = new FinishView();
            finish.Open();
            var cntr = new Container();
            var game = new GameView();
            game.Open();
            var match = new MatchView();
            match.Matched();
            match.Close();
            var sender = new FinishButtonEvents(match, finish, game);
            finish.button.Invoke();
            Assert.IsFalse(finish.View.activeInHierarchy);
            Assert.IsFalse(game.View.activeInHierarchy);
            Assert.IsTrue(match.View.activeInHierarchy);
        }
        [Test]
        public void testPlayEvents()
        {
            var socket = new LogSocket();
            var selector = new ChessSelectorView(new TestRay(), new BoardRaycastor(15, 15));
            var sender = new PlayEvents(selector,socket);
            selector.onPlace(new UnityEngine.Vector2Int(1, 2));
            Assert.AreEqual("Send opcode:5 (1,2)", socket.log);

        }
    }
}
