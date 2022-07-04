using System;
using System.Collections.Generic;
using Five;
using System.Reflection;

namespace FivesUnitTest
{
    class TestResponseSerializerRegister : ASerializerRegister
    {
        protected override int getCode(List<int> codes, int i)
        {
            return MessageCode.GetResponseCode(codes[i]);
        }

        protected override Type[] getSerTypes()
        {
            return new Type[]
            {
                typeof(MatchResponseSerializer),
                typeof(ResponseSerializer),
                typeof(ResponseSerializer)
            };
        }
        protected override bool isVailed(FieldInfo item)
        {
            return item.Name.StartsWith("Request");
        }
    }
}
