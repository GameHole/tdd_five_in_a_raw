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
    class TestPositionConvertor
    {
        PositionConvertor selector;
        [SetUp]
        public void SetUp()
        {
            selector = new PositionConvertor();
        }
        [Test]
        public void testToRemotePosition()
        {
            Assert.AreEqual(new Vector2Int(0, 0), selector.ToRemotePosition(new Vector3(-0.5f,0, -0.5f)));
            Assert.AreEqual(new Vector2Int(0, 0), selector.ToRemotePosition(new Vector3(0.499999f,0, 0.499999f)));
            Assert.AreEqual(new Vector2Int(1, 0), selector.ToRemotePosition(new Vector3(0.5f, 0, 0.49999f)));
        }
        [Test]
        public void testToWorldPosition()
        {
            Assert.AreEqual(new Vector3(0, 0, 0), selector.ToLocalPosition(new Vector2Int(0, 0)));
            Assert.AreEqual(new Vector3(1, 0, 2), selector.ToLocalPosition(new Vector2Int(1, 2)));
        }
    }
}
