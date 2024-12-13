using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    public class AppAssertion
    {
        private MessageProcesser app;

        public AppAssertion(MessageProcesser app)
        {
            this.app = app;
        }
        public void AssertProcesserIs<T>(int code)
        {
            IProcesser processer;
            Assert.IsTrue(app.TryGetValue(code, out processer),$"code:{code},type:{typeof(T).Name}");
            Assert.IsInstanceOf<T>(processer);
        }
    }
}
