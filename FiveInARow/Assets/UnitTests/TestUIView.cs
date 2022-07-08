using Five;
using NUnit.Framework;
using UnityEngine;

namespace UnitTests
{
    class TestUIView
    {
        [Test]
        public void testUIView()
        {
            var parent = PrefabHelper.Instantiate<Transform>("UI/Canvas");
            var view = new TestingUIView();
            view.Join(parent);
            Assert.AreSame(parent, view.View.transform.parent);
            Assert.AreEqual(new Vector3(0,0,0), view.View.transform.localPosition);
        }
    }
}
