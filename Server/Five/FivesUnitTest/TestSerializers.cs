using Five;
using NUnit.Framework;

namespace FivesUnitTest
{
    class TestSerializers
    {
        ByteStream stream;
        [SetUp]
        public void SetUp()
        {
            stream = new ByteStream();
        }
        [Test]
        public void testDefaultSerializer()
        {
            var serizer = new DefaultSerializer();
            serizer.Serialize(new Message { opcode = 1 }, stream);
            Assert.AreEqual(4, stream.Count);
            var msg = serizer.Deserialize(stream);
            Assert.AreEqual(1, msg.opcode);
        }
        [Test]
        public void testResponseSerializer()
        {
            var serizer = new ResponseSerializer();
            serizer.Serialize(new Response() { opcode = 1, result = 2 }, stream);
            Assert.AreEqual(8, stream.Count);
            var msg = serizer.Deserialize(stream) as Response;
            Assert.AreEqual(1, msg.opcode);
            Assert.AreEqual(2, msg.result);
        }
        [Test]
        public void testPlayMessageSerializer()
        {
            var serizer = new PlayMessageSerializer();
            serizer.Serialize(new PlayRequest { x = 1, y = 2 }, stream);
            Assert.AreEqual(12, stream.Count);
            var msg = serizer.Deserialize(stream) as PlayMessage;
            Assert.AreEqual(1, msg.x);
            Assert.AreEqual(2, msg.y);
        }
        [Test]
        public void testPlayNotifySerializer()
        {
            var serizer = new PlayNotifySerializer();
            serizer.Serialize(new PlayedNotify { x = 1, y = 2, id = 3 }, stream);
            Assert.AreEqual(16, stream.Count);
            var msg = serizer.Deserialize(stream) as PlayedNotify;
            Assert.AreEqual(1, msg.x);
            Assert.AreEqual(2, msg.y);
            Assert.AreEqual(3, msg.id);
        }
        [Test]
        public void testStartNotifySerializer()
        {
            var serizer = new StartNotifySerializer();
            serizer.Serialize(new StartNotify { playerId=5, infos= new PlayerInfo[] { new PlayerInfo(1, 2), new PlayerInfo(2, 3) } }, stream);
            var msg = serizer.Deserialize(stream) as StartNotify;
            Assert.AreEqual(5, msg.playerId);
            for (int i = 0; i < msg.infos.Length; i++)
            {
                Assert.AreEqual(i+1, msg.infos[i].Chess);
                Assert.AreEqual(i + 2, msg.infos[i].PlayerId);
            }
        }
        [Test]
        public void testPlayerIdNotifySerializer()
        {
            var serizer = new PlayerIdNotifySerializer();
            serizer.Serialize(new PlayerIdNotify(MessageCode.FinishNotify,3), stream);
            var msg = serizer.Deserialize(stream) as PlayerIdNotify;
            Assert.AreEqual(MessageCode.FinishNotify, msg.opcode);
            Assert.AreEqual(3, msg.playerId);
        }
        [Test]
        public void testOpCodeErrorSerializer()
        {
            var serizer = new OpCodeErrorSerializer();
            serizer.Serialize(new OpCodeErrorMessage(10), stream);
            var msg = serizer.Deserialize(stream) as OpCodeErrorMessage;
            Assert.AreEqual(SystemOpCode.UnknownOpCode, msg.opcode);
            Assert.AreEqual(10, msg.unknownOpcode);
        }
    }
}
