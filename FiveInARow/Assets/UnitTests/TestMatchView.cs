using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    class TestMatchView
    {
        MatchView matchView;
        [SetUp]
        public void SetUp()
        {
            matchView = new MatchView();
        }
        [Test]
        public void testMatchView()
        {
            var matchView = new MatchView();
            Assert.IsTrue(matchView.matchView.activeInHierarchy);
            Assert.False(matchView.cancelView.activeInHierarchy);
        }
        [Test]
        public void testMatch()
        {
            Assert.Fail();
        }
    }
}
