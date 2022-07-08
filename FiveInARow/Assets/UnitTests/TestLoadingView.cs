using Five;
using NUnit.Framework;
using UnityEngine;

namespace UnitTests
{
    class TestLoadingView
    {
        LoadingView main;
        [SetUp]
        public void SetUp()
        {
            main = new LoadingView();
        }
        [Test]
        public void testMainView()
        {
            Assert.IsTrue(main.View.activeInHierarchy);
        }
        [Test]
        public void testClose()
        {
            main.Close();
            Assert.IsFalse(main.View.activeInHierarchy);
        }
    }
}
