using NUnit.Framework;
using UnityEngine.UI;
using Five;
using UnityEngine;

namespace UnitTests
{
    class TestCountDownView
    {
        CountDownView count;
        Transform parent;
        [SetUp]
        public void SetUp()
        {
            parent = PrefabHelper.Instantiate<Transform>("UI/Canvas");
            count = new CountDownView(10);
            count.Join(parent);
        }
        [Test]
        public void testCountDownView()
        {
            Assert.AreEqual(10, count.Time);
            Assert.AreEqual(parent,count.View.transform.parent);
            Assert.IsTrue(count.View.activeSelf);
            Assert.AreEqual("10",count.TimeTxt);
        }
        [Test]
        public void testText()
        {
            count.TimeTxt = "1";
            Assert.AreEqual("1", count.TimeTxt);
            Assert.AreEqual("1", count.View.transform.Find("CountDownTxt").GetComponent<Text>().text);
        }
        [Test]
        public void testSetTime()
        {
            count.addingTime = 11;
            count.Update(0);
            Assert.AreEqual("0", count.TimeTxt);
            count.addingTime = 10;
            count.Update(0);
            Assert.AreEqual("0", count.TimeTxt);
            count.addingTime = 9;
            count.Update(0);
            Assert.AreEqual("1", count.TimeTxt);
        }
        [Test]
        public void testReset()
        {
            count.Reset();
            Assert.AreEqual(0, count.addingTime);
            Assert.AreEqual("10", count.TimeTxt);
        }
        [Test]
        public void testUpdate()
        {
            Assert.AreEqual(0, count.addingTime);
            count.Update(0.1f);
            Assert.AreEqual(0.1f, count.addingTime);
        }
    }
}
