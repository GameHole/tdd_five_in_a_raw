using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class TestChessBoard
    {
        readonly int TEST_COLOR = 1;
        private Chessboard board;
        private int W = 10;
        private int H = 10;
        [SetUp]
        public void SetUp()
        {
            board = new Chessboard(W, H);
        }
        [Test]
        public void testNewTable()
        {
            Assert.AreEqual(10, board.width);
            Assert.AreEqual(10, board.height);
        }
        [Test]
        public void testNewTableRnageException()
        {
            var ex = Assert.Throws<TableRangeException>(() =>
              {
                  new Chessboard(5, 5);
              });
            Assert.AreEqual("Table's width or height must be greater then five", ex.Message);
        }
        [Test]
        public void testGetValue()
        {
            Assert.AreEqual(0, board.GetValue(0, 0));
        }
        [Test]
        public void testGetValueException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                board.GetValue(W, H);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                board.GetValue(-1, -1);
            });
        }
        [Test]
        public void testSetValueException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                board.AddValue(W, H, 0);
            });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                board.AddValue(-1, -1, 0);
            });
        }
        [Test]
        public void testSetValueColorException()
        {
            var ex = Assert.Throws<ChessColorException>(() =>
              {
                  board.AddValue(0, 0, 0);
              });
            Assert.AreEqual("Chess color must greater then zero", ex.Message);
        }

        [Test]
        public void testAddValue()
        {
            Assert.IsTrue(board.AddValue(1, 0, TEST_COLOR));
            Assert.AreEqual(TEST_COLOR, board.GetValue(1, 0));
            Assert.IsTrue(board.AddValue(0, 4, 2));
            Assert.AreEqual(2, board.GetValue(0, 4));
        }
        [Test]
        public void testAddValueOnSamePos()
        {
            board.AddValue(1, 0, TEST_COLOR);
            Assert.IsFalse(board.AddValue(1, 0, 2));
            Assert.AreEqual(1, board.GetValue(1,0));
        }
        [Test]
        public void testToIndex()
        {
            Assert.AreEqual(1 * 10 + 0, board.toIndex(1, 0));
            Assert.AreEqual(0 * 10 + 1, board.toIndex(0, 1));
        }
        [Test]
        public void testIsFiveInRow_Default_False()
        {
            Assert.IsFalse(board.isFiveInRow(TEST_COLOR));
        }
        [Test]
        public void testIsFiveInRow_H()
        {
            for (int y = 0; y < H; y++)
            {
                for (int x = 0; x < W - 5; x++)
                {
                    var table = new Chessboard(W, H);
                    for (int i = 0; i < 5; i++)
                    {
                        table.AddValue(x + i, y, TEST_COLOR);
                    }
                    Assert.IsTrue(table.isFiveInRow(TEST_COLOR));
                }
            }
        }
        [Test]
        public void testIsFiveInRow_V()
        {
            for (int x = 0; x < W; x++)
            {
                for (int y = 0; y < H-5; y++)
                {
                    var table = new Chessboard(W, H);
                    for (int i = 0; i < 5; i++)
                    {
                        table.AddValue(x, y + i, TEST_COLOR);
                    }
                    Assert.IsTrue(table.isFiveInRow(TEST_COLOR));
                }
            }
        }
        [Test]
        public void testIsFiveInRow_Main_Diagonal()
        {
            for (int x = 0; x < W-5; x++)
            {
                for (int y = 0; y < H-5; y++)
                {
                    var table = new Chessboard(W, H);
                    for (int i = 0; i < 5; i++)
                    {
                        table.AddValue(x+i, y+i, TEST_COLOR);
                    }
                    Assert.IsTrue(table.isFiveInRow(TEST_COLOR));
                }
            }
        }
        [Test]
        public void testIsFiveInRow_Minor_Diagonal()
        {
            for (int x = 0; x < W - 5; x++)
            {
                for (int y = 0; y < H - 5; y++)
                {
                    var table = new Chessboard(W, H);
                    for (int i = 0; i < 5; i++)
                    {
                        table.AddValue(x+i, y+4-i, TEST_COLOR);
                    }
                    Assert.IsTrue(table.isFiveInRow(TEST_COLOR));
                }
            }
        }
        [Test]
        public void testIsFiveInRow_Exception()
        {
            Assert.Throws<ChessColorException>(() =>
            {
                Assert.IsTrue(board.isFiveInRow(0));
            });
        }
    }
}
