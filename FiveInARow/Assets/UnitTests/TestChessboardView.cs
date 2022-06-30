﻿using System.Collections;
using Five;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnitTests
{
    public class TestChessboardView
    {
        ChessboardView view;
        PositionConvertor convertor;
        [SetUp]
        public void SetUp()
        {
            convertor = new PositionConvertor();
            view = new ChessboardView(convertor);
        }
        [Test]
        public void testChessboardView()
        {
            Assert.AreEqual(0, view.Count);
        }
        [Test]
        public void testGetChess()
        {
            var chessView = view.GetChess(0, 0);
            Assert.IsNull(chessView);

        }
        [Test]
        public void testSetChess()
        {
            view.SetChess(1, 2, 1);
            var chessView = view.GetChess(1, 2);
            Assert.AreEqual(convertor.ToLocalPosition(new Vector2Int(1, 2)), chessView.transform.position);
            Assert.AreEqual(new Color(0, 0, 0, 1), chessView.color);
        }
        [Test]
        public void testSetChessException()
        {
            view.SetChess(1, 2, 1);
            var ex = Assert.Throws<PositionPlacedChessException>(() =>
             {
                 view.SetChess(1, 2, 1);
             });
            Assert.AreEqual("You can not place chess at poisition (1,2) ,because position allready placeing a chess.", ex.Message);
        }
        [Test]
        public void testClearArray()
        {
            view.Clear();
            Assert.AreEqual(0, view.Count);
        }
        [UnityTest]
        public IEnumerator testClearChess()
        {
            view.SetChess(1, 2, 1);
            var chessView = view.GetChess(1, 2);
            view.Clear();
            yield return null;
            Assert.IsFalse(chessView);
        }
    }
}