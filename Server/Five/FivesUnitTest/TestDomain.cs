using Five;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    internal class TestDomain
    {
        [Test]
        public void test()
        {
            var domain = new Domain(new TGameFactroy(), new TIdGenrator());
            Assert.NotNull(domain.matchServce);
            Assert.NotNull(domain.loginServce);
            Assert.NotNull(domain.gameServce);
        }
    }
}
