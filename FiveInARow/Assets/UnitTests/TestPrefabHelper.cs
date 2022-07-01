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
    class TestPrefabHelper
    {
        [Test]
        public void testInstantiate()
        {
            var prefab = PrefabHelper.Instantiate<GameObject>("GameObjects/Chess");
            Assert.NotNull(prefab);
        }
        [Test]
        public void testInstantiateWithParent()
        {
            var parent = new GameObject().transform;
            var prefab = PrefabHelper.Instantiate<GameObject>("GameObjects/Chess", parent);
            Assert.AreEqual(parent,prefab.transform.parent);
        }
        [Test]
        public void testFindException()
        {
            var ex = Assert.Throws<PrefabNotFoundException>(() =>
            {
                var prefab = PrefabHelper.Find("no_this_prefab");
            });
            Assert.AreEqual("Prefab 'no_this_prefab' with type <UnityEngine.GameObject> not found ", ex.Message);
        }
        
    }
}
