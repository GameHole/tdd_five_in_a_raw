using Five;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TestTools;

namespace UnitTests
{
    class TestAppRunner
    {
        AppRunner runner;
        [SetUp]
        public void SetUp()
        {
            runner = new GameObject().AddComponent<AppRunner>();
        }
        [UnityTest]
        public IEnumerator testRunner()
        {
            var threadTaker = new GameObject().AddComponent<ThreadIdTaker>();
            yield return null;
            Assert.NotNull(runner.app);
            Assert.NotNull(runner.socket);
            Assert.AreEqual(typeof(TcpSocket),runner.socket.GetType());
            Assert.IsTrue(runner.app.IsStarted);
            var client = runner.app.client;
            var mainProcesser = new LogMainThreadProcesser();
            client.Procsesser.Processers.Add(mainProcesser.OpCode, mainProcesser);
            runner.socket.onRecv(new Message(mainProcesser.OpCode));
            yield return null;
            Assert.AreEqual(threadTaker.threadId, mainProcesser.threadId);
        }
        [UnityTest]
        public IEnumerator testRunUpdater()
        {
            var logupdater = new LogUpdater();
            runner.app.updaters.Add(logupdater);
            yield return null;
            Assert.IsTrue(logupdater.isRun);
        }
       
        [UnityTest]
        public IEnumerator testStartNotify()
        {
            yield return null;
            runner.socket.onRecv(new StartNotify() { infos=new PlayerInfo[] {new PlayerInfo(1,0),new PlayerInfo(2,1) } });
            yield return null;
            Assert.IsTrue(runner.app.cntr.Get<GameView>().View.activeInHierarchy);
        }
    }
}
