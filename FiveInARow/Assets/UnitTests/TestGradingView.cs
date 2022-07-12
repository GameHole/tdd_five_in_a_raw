using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace UnitTests
{
    class TestGradingView
    {
        GradingView view;
        [SetUp]
        public void SetUp()
        {
            view = new GradingView(15, 20);
        }
        [Test]
        public void testGradingView()
        {
            Assert.AreEqual(15, view.width);
            Assert.AreEqual(20, view.height);
            Assert.AreEqual(0.03f, view.gradingWidth, 0.0001f);
            GameObject unitview = view.View;
            Assert.NotNull(unitview);
            var griddingh = view.griddingH;
            Assert.AreEqual(view.height + 1, griddingh.Count);
            for (int i = 0; i < griddingh.Count; i++)
            {
                var grad = griddingh[i];
                Assert.AreEqual(new Vector3(view.width, 1, view.gradingWidth), grad.localScale);
                Assert.AreEqual(new Vector3(view.width * 0.5f, 0, i), grad.position);
            }
            var griddingv = view.griddingV;
            Assert.AreEqual(view.width + 1, griddingv.Count);
            for (int i = 0; i < griddingv.Count; i++)
            {
                var grad = griddingv[i];
                Assert.AreEqual(new Vector3(view.gradingWidth, 1, view.height), grad.localScale);
                Assert.AreEqual(new Vector3(i, 0, view.height * 0.5f), grad.position);
            }
        }
        [Test]
        public void testBg()
        {
            var bg = view.BG;
            Assert.AreEqual(new Vector3(7.5f, -0.001f, 10f), bg.position);
            Assert.AreEqual(new Vector3(15 + 1, 1, 20 + 1), bg.localScale);
        }
    }
}
