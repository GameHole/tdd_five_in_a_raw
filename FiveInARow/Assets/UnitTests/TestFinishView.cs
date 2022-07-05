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
    class TestFinishView
    {
        FinishView view;
        [SetUp]
        public void SetUp()
        {
            view = new FinishView();
            view.Join(PrefabHelper.Instantiate<Transform>("UI/Canvas"));
        }
        [Test]
        public void testFinishView()
        {
            Assert.IsFalse(view.View.activeInHierarchy);
        }
        [Test]
        public void testWinLoss()
        {
            view.IsWin = true;
            Assert.AreEqual("you win", view.text.text);
            view.IsWin = false;
            Assert.AreEqual("you loss", view.text.text);
        }
    }
}
