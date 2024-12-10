using Five;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    class TestApp
    {
        private Server server;
        Domain app;
        private MatchServce servce;
        private LogRequestRegister log;
        Five.Client[] clients;
        public static readonly int port = 11000;
        [SetUp]
        public void SetUp()
        {
            var factroy = new NetFactroy(new SerializerRegister(),new SocketFactroy());
            app = new Domain(new GameFactroy(),new IdGenrator());
            servce = new MatchServce(app);
            log = new LogRequestRegister(servce);
            server = factroy.NewServer("127.0.0.1", port, log);
            server.StartAsync();
            clients = new Client[2];
            for (int i = 0; i < clients.Length; i++)
            {
                clients[i] = factroy.NewClient();
            }
        }
        [TearDown]
        public void TearDown()
        {
            for (int i = 0; i < clients.Length; i++)
            {
                clients[i].Close();
            }
            server.Stop();
        }
        [Test]
        public void testApp()
        {
            Assert.NotNull(app.playerRsp);
            Assert.NotNull(app.roomRsp);
        }
        [Test]
        public void testClientRsp()
        {
            Assert.AreSame(server.processer, log.processer);
            Assert.AreEqual(typeof(OpCodeErrorResponseProcesser), server.processer.defaultProcesser.GetType());
            Assert.AreEqual(typeof(ConnectProcesser),server.processer.connect.GetType());
            Assert.AreEqual(typeof(ServerStopProcesser), server.processer.serverStop.GetType());
            Assert.IsTrue(server.processer.Processers.Contains(MessageCode.RequestMatch));
            Assert.IsTrue(server.processer.Processers.Contains(MessageCode.RequestCancelMatch));
            Assert.IsTrue(server.processer.Processers.Contains(MessageCode.RequestPlay));
            Assert.IsTrue(server.processer.Processers.Contains(-1));
        }
        [Test]
        public async Task testonAccept()
        {
            await Task.Delay(100);
            clients[0].Connect("127.0.0.1", port);
            await Task.Delay(100);
            var socket = server.clients.First();

            Assert.AreSame(servce, log.test.Mgr);
            Assert.AreSame(socket.processer, log.processer);
            socket.processer.Process(socket,new Message { opcode = -1 });
            Assert.AreSame(socket, log.test.msgSock);

        }
        [Test]
        public async Task testRun()
        {
            await Task.Delay(100);
            clients[0].Connect("127.0.0.1", port);
            await Task.Delay(100);
            var log = LogProcesser.mockProcesser(clients[0],2);
            clients[0].Send(new Message(MessageCode.RequestMatch));
            await Task.Delay(100);
            Assert.NotNull(log.msg);
            server.Stop();
            Assert.AreEqual(0, app.roomRsp.GameCount);
            Assert.AreEqual(0, app.playerRsp.Count);
            Assert.IsFalse(server.IsRun);
        }
        [Test]
        public async Task testGameFlow()
        {
            await Task.Delay(100);
            for (int i = 0; i < clients.Length; i++)
            {
                clients[i].Connect("127.0.0.1", port);
            }
            ConcurrentQueue<Message>[] messages = MakeMessageQeueus();
            for (int i = 0; i < 2; i++)
            {
                await AssertOneFlow(messages);
            }
        }

        private ConcurrentQueue<Message>[] MakeMessageQeueus()
        {
            ConcurrentQueue<Message>[] messages = new ConcurrentQueue<Message>[clients.Length];
            for (int i = 0; i < clients.Length; i++)
            {
                var delay = new DelayMessageProcesser(default);
                clients[i].processer = delay;
                messages[i] = delay.queue;
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
            for (int i = 0; i < clients.Length; i++)
            {
                await Task.Delay(100);
                clients[i].Send(new Message(MessageCode.RequestMatch));
            };
        }

        private async Task PlayToFinish()
        {
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < clients.Length; i++)
                {
                    await Task.Delay(100);
                    clients[i].Send(new PlayRequest() { x = j, y = i });
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

        [Test]
        public async Task testClientOutLine()
        {
            await Task.Delay(100);
            for (int i = 0; i < clients.Length; i++)
            {
                clients[i].Connect("127.0.0.1", port);
            }
            for (int i = 0; i < clients.Length; i++)
            {
                await Task.Delay(100);
                clients[i].Send(new Message(MessageCode.RequestMatch));
            }
            await Task.Delay(100);
            var game = app.roomRsp.GetRoom(1);
            Assert.IsTrue(game.IsRunning);
            for (int i = 0; i < clients.Length; i++)
            {
                clients[i].Close();
            }
            await Task.Delay(100);
            Assert.IsFalse(game.IsRunning);
        }
    }
}
