using System;
using Five;
using NUnit.Framework;
using UnityEngine;

namespace UnitTests
{
    class LogFlow : IGameFinish, IGameStart, IPlayerPlay, IPlayerTurn
    {
        public string log;

        public void Finish(int winChess)
        {
            log += $"Finish chess:{winChess} ";
        }

        public void Play(int chess, Vector2Int pos)
        {
            log += $"Play chess:{chess} Pos:({pos.x},{pos.y}) ";
        }

        public void Start(int selfChess)
        {
            log += $"Start chess:{selfChess} ";
        }
        
        public void Turn(int chess)
        {
            log += $"Turn chess:{chess} ";
        }
    }
    class TestGameFlow
    {
       
        [Test]
        public void testGameFlow()
        {
            var flow = new GameFlow();
            Assert.AreEqual(4, flow.Count);
            AssertCount(0,flow);
        }

        private static void AssertCount(int count,GameFlow flow)
        {
            Assert.AreEqual(count, flow.GetFlowList<IGameStart>().Count);
            Assert.AreEqual(count, flow.GetFlowList<IPlayerTurn>().Count);
            Assert.AreEqual(count, flow.GetFlowList<IPlayerPlay>().Count);
            Assert.AreEqual(count, flow.GetFlowList<IGameFinish>().Count);
        }

        [Test]
        public void testAdd()
        {
            var flow = new GameFlow();
            flow.AddFlow(new LogFlow());
            AssertCount(1, flow);
        }
        [Test]
        public void testFlow()
        {
            var flow = new GameFlow();
            var log = new LogFlow();
            flow.AddFlow(log);
            OneFlow(flow);
            Assert.AreEqual("Start chess:1 Play chess:0 Pos:(0,0) Turn chess:2 Finish chess:1 ", log.log);
        }
        [Test]
        public void testContain()
        {
            var flow = new GameFlow();
            Assert.IsFalse(flow.Contain(typeof(LogFlow)));
            var log = new LogFlow();
            flow.AddFlow(log);
            Assert.IsTrue(flow.Contain(typeof(LogFlow)));
        }
        private void OneFlow(GameFlow flow)
        {
            foreach (var item in flow.GetFlowList<IGameStart>())
            {
                item.Start(1);
            }
            foreach (var item in flow.GetFlowList<IPlayerPlay>())
            {
                item.Play(0,new Vector2Int(0,0));
            }
            foreach (var item in flow.GetFlowList<IPlayerTurn>())
            {
                item.Turn(2);
            }
            foreach (var item in flow.GetFlowList<IGameFinish>())
            {
                item.Finish(1);
            }
        }
    }
}
