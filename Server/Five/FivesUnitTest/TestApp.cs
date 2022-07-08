using Five;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    class TestApp
    {
        App app;
        TcpSocket[] sockets;
        [SetUp]
        public void SetUp()
        {
            app = new App(new Server("127.0.0.1", 10000));
            app.StartAsync();
            var reg = new SerializerRegister();
            sockets = new TcpSocket[2];
            for (int i = 0; i < sockets.Length; i++)
            {
                sockets[i] = new TcpSocket(reg);
            }
        }
        [TearDown]
        public void TearDown()
        {
            for (int i = 0; i < sockets.Length; i++)
            {
                sockets[i].Close();
            }
            app.Stop();
        }
        [Test]
        public void testApp()
        {
            Assert.NotNull(app.server);
            Assert.NotNull(app.mgr);
            Assert.NotNull(app.matching);
            Assert.NotNull(app.server.onAccept);
        }
        [Test]
        public async Task testRun()
        {
            await Task.Delay(100);
            sockets[0].Connect("127.0.0.1", 10000);
            await Task.Delay(100);
            Assert.AreEqual(1, app.mgr.clients.Count);
            var log = "";
            sockets[0].onRecv += (msg) => log += $"msg:{msg.opcode}";
            sockets[0].Send(new Message(MessageCode.RequestMatch));
            await Task.Delay(100);
            Assert.AreEqual($"msg:{MessageCode.GetResponseCode(MessageCode.RequestMatch)}", log);
            app.Stop();
            Assert.AreEqual(0, app.matching.GameCount);
            Assert.AreEqual(0, app.mgr.clients.Count);
            Assert.IsFalse(app.server.IsRun);
        }
        [Test]
        public async Task testGameFlow()
        {
            await Task.Delay(100);
            for (int i = 0; i < sockets.Length; i++)
            {
                sockets[i].Connect("127.0.0.1", 10000);
            }
            ConcurrentQueue<Message>[] messages = MakeMessageQeueus();
            for (int i = 0; i < 2; i++)
            {
                await AssertOneFlow(messages);
            }
        }

        private ConcurrentQueue<Message>[] MakeMessageQeueus()
        {
            ConcurrentQueue<Message>[] messages = new ConcurrentQueue<Message>[sockets.Length];
            for (int i = 0; i < sockets.Length; i++)
            {
                var queue = new ConcurrentQueue<Message>();
                sockets[i].onRecv += (msg) =>
                {
                    queue.Enqueue(msg);
                };
                messages[i] = queue;
            }

            return messages;
        }

        private async Task AssertOneFlow(ConcurrentQueue<Message>[] messages)
        {
            ClearQueues(messages);
            await Match();
            await PlayToFinish();
            await Task.Delay(500);
            LogMessageQueue(messages[1]);
            Func<List<int>>[] messageFlow = new Func<List<int>>[]
            {
                GetPlayerFirstFlow,GetPlayerSecondFlow
            };
            List<int>[] excepts = new List<int>[2];
            for (int i = 0; i < excepts.Length; i++)
            {
                excepts[i] = messageFlow[i].Invoke();
            }
            for (int i = 0; i < messages.Length; i++)
            {
                Assert.AreEqual(excepts[i].Count, messages[i].Count);
                for (int j = 0; j < messages[i].Count; j++)
                {
                    messages[i].TryDequeue(out var msg);
                    Assert.AreEqual(excepts[i][j], msg.opcode);
                }
            }
        }

        private async Task Match()
        {
            for (int i = 0; i < sockets.Length; i++)
            {
                await Task.Delay(100);
                sockets[i].Send(new Message(MessageCode.RequestMatch));
            };
        }

        private async Task PlayToFinish()
        {
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < sockets.Length; i++)
                {
                    await Task.Delay(100);
                    sockets[i].Send(new PlayRequest() { x = j, y = i });
                }
            }
        }

        List<int> GetPlayerFirstFlow()
        {
            var excepts = new List<int>();
            excepts.Add(MessageCode.GetResponseCode(MessageCode.RequestMatch));
            excepts.Add(MessageCode.StartNotify);
            excepts.Add(MessageCode.TurnNotify);

            for (int j = 0; j < 4; j++)
            {
                AddPlayRes(excepts);
                AddPlayNtf(excepts);
            }
            excepts.Add(MessageCode.PlayedNotify);
            excepts.Add(MessageCode.FinishNotify);
            excepts.Add(MessageCode.GetResponseCode(MessageCode.RequestPlay));
            return excepts;
        }
        List<int> GetPlayerSecondFlow()
        {
            var excepts = new List<int>();

            excepts.Add(MessageCode.StartNotify);
            excepts.Add(MessageCode.TurnNotify);
            excepts.Add(MessageCode.GetResponseCode(MessageCode.RequestMatch));
           
            for (int j = 0; j < 4; j++)
            {
                AddPlayNtf(excepts);
                AddPlayRes(excepts);
            }
            excepts.Add(MessageCode.PlayedNotify);
            excepts.Add(MessageCode.FinishNotify);
            excepts.Add(MessageCode.GetResponseCode(MessageCode.RequestPlay));
            return excepts;
        }
        private void ClearQueues(ConcurrentQueue<Message>[] messages)
        {
            foreach (var item in messages)
            {
                item.Clear();
            }
        }

        private void LogMessageQueue(ConcurrentQueue<Message> messages)
        {
            foreach (var item in messages)
            {
                Console.WriteLine(item);
            }
        }

        private void AddPlayRes(List<int> excepts)
        {
            AddPlayNtf(excepts);
            excepts.Add(MessageCode.GetResponseCode(MessageCode.RequestPlay));
        }

        private void AddPlayNtf(List<int> excepts)
        {
            excepts.Add(MessageCode.PlayedNotify);
            excepts.Add(MessageCode.TurnNotify);
        }
    }
}
