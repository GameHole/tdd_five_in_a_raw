using Five;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnitTests
{
    public class TestButtonEvent
    {
        ButtonEvent eve;
        Button btn;
        [SetUp]
        public void SetUp()
        {
            btn = new GameObject().AddComponent<Button>();
            eve = new ButtonEvent(btn);
        }
        [Test]
        public void testEvent()
        {
            Assert.AreEqual(0, eve.EventCount);
        }
        [Test]
        public void testAdd()
        {
            eve.AddListener(() => { });
            Assert.AreEqual(1, eve.EventCount);
        }
        [Test]
        public void testRemove()
        {
            Action e = () => { };
            eve.AddListener(e);
            eve.RemoveListener(e);
            Assert.AreEqual(0, eve.EventCount);
        }
        [Test]
        public void testInvoke()
        {
            bool isInvoked = false;
            Action e = () => isInvoked = true;
            eve.AddListener(e);
            eve.Invoke();
            Assert.IsTrue(isInvoked);
        }
        [Test]
        public void testButtonInvoke()
        {
            bool isInvoked = false;
            Action e = () => isInvoked = true;
            eve.AddListener(e);
            btn.onClick.Invoke();
            Assert.IsTrue(isInvoked);
        }
    }
}
