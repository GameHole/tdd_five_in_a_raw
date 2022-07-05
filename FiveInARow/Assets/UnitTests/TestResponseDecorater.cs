using Five;
using NUnit.Framework;

namespace UnitTests
{
    class TestResponseDecorater
    {
        LogProcesser processer;
        ResponseDecorater dec;
        Response response;
        LogLoger log;
        [SetUp]
        public void SetUp()
        {
            processer = new LogProcesser();
            log = new LogLoger();
            dec = new ResponseDecorater(processer, log);
            response = new Response(9);
        }
        [Test]
        public void testFailedResponse()
        {
            Assert.AreEqual(processer.OpCode, dec.OpCode);
            response.result = 10001;
            dec.Process(response);
            Assert.AreEqual("Response error,result:10001", log.errorLog);
     
        }
        [Test]
        public void testSuccessResponse()
        {
            response.result = 0;
            dec.Process(response);
            Assert.AreEqual("Process", processer.log);
        }
    }
}
