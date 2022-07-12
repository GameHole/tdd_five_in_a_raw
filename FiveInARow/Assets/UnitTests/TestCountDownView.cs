using NUnit.Framework;
using UnityEngine.UI;
using Five;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
namespace UnitTests
{
    class TestCountDownView
    {
        CountDownView count;
        [SetUp]
        public void SetUp()
        {
            count = new CountDownView(10);
            count.Join(PrefabHelper.Instantiate<Transform>("UI/Canvas"));
        }
        [Test]
        public void testCountDownView()
        {
            Assert.AreEqual(10, count.Time);
            Assert.IsTrue(count.View.activeSelf);
            Assert.AreEqual("10",count.TimeTxt);

        }
        [Test]
        public void testViewPosirion()
        {
            var rectTran = count.View.transform.Find("View").GetComponent<RectTransform>();
            Assert.AreEqual(new Vector2(0.5f, 1f), rectTran.anchorMin);
            Assert.AreEqual(new Vector2(0.5f, 1f), rectTran.anchorMax);
            Assert.AreEqual(new Vector2(0f, -71f), rectTran.anchoredPosition);
        }
        [Test]
        public void testText()
        {
            count.TimeTxt = "1";
            Assert.AreEqual("1", count.TimeTxt);
            Assert.AreEqual("1", count.View.GetComponentInChildren<Text>().text);
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
