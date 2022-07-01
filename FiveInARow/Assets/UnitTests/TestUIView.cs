using NUnit.Framework;
using UnityEngine;

namespace UnitTests
{
    class TestUIView
    {
        [Test]
        public void testUIView()
        {
            var parent = new GameObject().transform;
            var view = new TestingUIView();
            view.Join(parent);
            Assert.AreSame(parent, view.View.transform.parent);
        }
    }
}
