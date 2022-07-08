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
        [Test]
        public void testGradingView()
        {
            var view = new GradingView(15, 20);
            Assert.AreEqual(15, view.width);
            Assert.AreEqual(20, view.height);
            Assert.AreEqual(0.015f, view.gradingWidth, 0.0001f);
            GameObject unitview = view.View;
            Assert.NotNull(unitview);
            var griddingh = view.griddingH;
            Assert.AreEqual(view.height+1, griddingh.Count);
            for (int i = 0; i < griddingh.Count; i++)
            {
                var grad = griddingh[i];
                Assert.AreEqual(new Vector3(view.width, 1, view.gradingWidth), grad.localScale);
                Assert.AreEqual(new Vector3(view.width * 0.5f, 0, i), grad.position);
            }
            var griddingv = view.griddingV;
            Assert.AreEqual(view.width+1, griddingv.Count);
            for (int i = 0; i < griddingv.Count; i++)
            {
                var grad = griddingv[i];
                Assert.AreEqual(new Vector3(view.gradingWidth, 1, view.height), grad.localScale);
                Assert.AreEqual(new Vector3(i, 0, view.height * 0.5f), grad.position);
            }
        }
    }
}
