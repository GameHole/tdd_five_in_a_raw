using Five;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnitTests
{
    class TestApp
    {
        App app;
        LogSocket socket;
        Ip ip;
        [SetUp]
        public void SetUp()
        {
            socket = new LogSocket();
            ip = new Ip { address = "1.1.1.1", port = 0 };
            app = new App(socket, ip);
        }
        [Test]
        public void testApp()
        {
            Assert.NotNull(app.loger);
            var cntr = app.cntr;
            Type[] checkTypes = new Type[]
            {
                typeof(LoadingView),
                typeof(MatchView),
                typeof(GameView),
                typeof(ChessboardView),
                typeof(GradingView),
                typeof(CountDownView),
                typeof(FinishView),
                typeof(ChessSelectorView),
                typeof(ChessPlacer),
                typeof(Player),
                typeof(PlayersInfo)
            };
            foreach (var item in checkTypes)
            {
                Assert.IsTrue(cntr.Contain(item));
            }
        }
        [Test]
        public void testFlowRegisted()
        {
            Assert.IsTrue(app.gameFlow.Contain(typeof(SelfChessSetter)));
        }

        [Test]
        public void testView()
        {
            Assert.NotNull(app.cntr.Get<LoadingView>().View.transform.parent.GetComponent<Canvas>());
            Assert.NotNull(app.cntr.Get<MatchView>().View.transform.parent.GetComponent<Canvas>());
        }
        [Test]
        public void testEnent()
        {
            Assert.AreEqual(1,app.cntr.Get<ChessSelectorView>().onPlace.GetInvocationList().Length);
            var matchView = app.cntr.Get<MatchView>();
            Assert.AreEqual(1, matchView.cancelBtn.EventCount);
            Assert.AreEqual(1, matchView.matchBtn.EventCount);
            var finish = app.cntr.Get<FinishView>();
            Assert.AreEqual(1, finish.button.EventCount);
        }
        [UnityTest]
        public IEnumerator testStart()
        {
            Assert.IsFalse(app.IsStarted);
            app.Start();
            Assert.IsTrue(app.IsStarted);
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual($"Connect {ip}", socket.log);
            Assert.IsFalse(app.cntr.Get<LoadingView>().View.activeInHierarchy);
            Assert.IsTrue(app.cntr.Get<MatchView>().View.activeInHierarchy);
        }
        [UnityTest]
        public IEnumerator testFailedStart()
        {
            var app = new App(new ErrorSocket(), ip);
            var loger = new LogLoger();
            app.loger = loger;
            app.Start();
            yield return new WaitForSeconds(0.1f);
            Assert.AreEqual("Connect Error", loger.logStr);
            Assert.IsTrue(app.cntr.Get<LoadingView>().View.activeInHierarchy);
            Assert.IsFalse(app.cntr.Get<MatchView>().View.activeInHierarchy);
        }
        [Test]
        public void testRegistedUpdater()
        {
            List<IUpdate> updates = new List<IUpdate>();
            updates.Add(app.client);
            updates.Add(app.cntr.Get<CountDownView>());
            updates.Add(app.cntr.Get<ChessSelectorView>());
            updates.Add(app.cntr.Get<ChessPlacer>());
            foreach (var item in updates)
            {
                Assert.IsTrue(app.updaters.Contains(item),item.ToString());
            }
        }
    }
}
