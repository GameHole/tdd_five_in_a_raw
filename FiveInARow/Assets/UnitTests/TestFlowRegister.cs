using NUnit.Framework;
using Five;
using System;

namespace UnitTests
{
    class TestFlowRegister
    {
        [Test]
        public void TestRegisted()
        {
            Type[] need = new Type[]
            {
                typeof(SelfChessSetter),
                typeof(StartViewController),
                typeof(ChessboardViewController),
                typeof(FinishViewController),
                typeof(CountDownViewController),
                typeof(ChessSelectorController),
                typeof(TurnChessTypeSetter)
            };
            var cntr = new Container();
            new GameBuilder(1, 1, null).Build(cntr);
            var reg = new FlowRegister(cntr);
            var flow = new GameFlow();
            reg.Regist(flow);
            foreach (var item in need)
            {
                Assert.IsTrue(flow.Contain(item),item.ToString());
            }
        }
    }
}
