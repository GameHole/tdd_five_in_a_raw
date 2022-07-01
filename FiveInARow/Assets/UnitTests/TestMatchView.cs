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
    class TestMatchView
    {
        MatchView matchView;
        Transform parent;
        [SetUp]
        public void SetUp()
        {
            parent = PrefabHelper.Instantiate<Transform>("UI/Canvas");
            matchView = new MatchView();
            matchView.Join(parent);
        }
        [Test]
        public void testMatchView()
        {
            Assert.IsFalse(matchView.View.activeInHierarchy);
        }
        [Test]
        public void testOpen()
        {
            matchView.Open();
            Assert.IsTrue(matchView.View.activeInHierarchy);
            Assert.IsTrue(matchView.MatchingView.activeSelf);
            Assert.IsFalse(matchView.CancelView.activeSelf);
        }
        [Test]
        public void testClose()
        {
            matchView.Close();
            Assert.IsFalse(matchView.View.activeInHierarchy);
        }
        [Test]
        public void testMatch()
        {
            matchView.Matched();
            Assert.IsFalse(matchView.MatchingView.activeSelf);
            Assert.IsTrue(matchView.CancelView.activeSelf);
        }
        [Test]
        public void testCanceled()
        {
            matchView.Canceled();
            Assert.IsTrue(matchView.MatchingView.activeSelf);
            Assert.IsFalse(matchView.CancelView.activeSelf);
        }
    }
}
