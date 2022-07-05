using NUnit.Framework;
using System;
using System.Collections.Generic;
using Five;
using System.Reflection;

namespace FivesUnitTest
{
    abstract class ASerializerRegister
    {
        protected MessageSerializer ser;
        [SetUp]
        public void SetUp()
        {
            ser = new MessageSerializer();
            new SerializerRegister().Regist(ser);
        }
        [Test]
        public void testRegister()
        {
            List<int> codes = getCodes();
            Type[] types = getSerTypes();
            Assert.AreEqual(codes.Count, types.Length);
            for (int i = 0; i < codes.Count; i++)
            {
                int code = getCode(codes, i);
                Assert.IsTrue(ser.Container.Contains(code), code.ToString());
                Assert.AreEqual(types[i], ser.GetSerializer(code).GetType());
            }
        }
        public List<int> getCodes()
        {
            var fields = typeof(MessageCode).GetFields(BindingFlags.Public | BindingFlags.Static);
            List<int> codes = new List<int>();
            foreach (var item in fields)
            {
                if (isVailed(item))
                {
                    int code = (int)item.GetValue(null);
                    if (code > 0)
                        codes.Add(code);
                }
            }

            return codes;
        }

        protected abstract bool isVailed(FieldInfo item);
        protected abstract Type[] getSerTypes();
        protected abstract int getCode(List<int> codes, int i);
    }
}
