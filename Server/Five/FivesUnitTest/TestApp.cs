using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FivesUnitTest
{
    class TestApp
    {
        [Test]
        public async Task testApp()
        {
            var app = new App(new Server("127.0.0.1",10000));
            app.StartAsync();
            var socket = new TcpSocket();
            await Task.Delay(100);
            socket.Connect("127.0.0.1", 10000);
            await Task.Delay(100);
            Assert.AreEqual(1,app.mgr.ClientCount);
            var log = "";
            socket.onRecv += (msg) => log += $"msg:{msg.opcode}";
            socket.Send(new Message(MessageCode.RequestMatch));
            await Task.Delay(100);
            Assert.AreEqual($"msg:{MessageCode.GetResponseCode(MessageCode.RequestMatch)}", log);
            app.Stop();
            Assert.AreEqual(0, app.matching.GameCount);
            Assert.AreEqual(0, app.mgr.ClientCount);
            Assert.IsFalse(app.server.IsRun);
        }
    }
}
