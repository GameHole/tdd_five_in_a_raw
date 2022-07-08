using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    class TestSelfChessView
    {
        [Test]
        public void testSelfChessView()
        {
            var chess = new SelfChessView();
            Assert.NotNull(chess.View.GetComponent<ChessView>());
        }
    }
}
