using System;
using System.Collections.Generic;
using Five;
using System.Reflection;

namespace FivesUnitTest
{
    class TestMessageSerializerRegister : ASerializerRegister
    {
        protected override int getCode(List<int> codes, int i)
        {
            return codes[i];
        }

        protected override bool isVailed(FieldInfo item)
        {
            return true;
        }

        protected override Type[] getSerTypes()
        {
            return new Type[]
            {
                typeof(DefaultSerializer),
                typeof(DefaultSerializer),
                typeof(PlayMessageSerializer),
                typeof(StartNotifySerializer),
                typeof(PlayNotifySerializer),
                typeof(PlayerIdNotifySerializer),
                typeof(PlayerIdNotifySerializer)
            };
        }
    }
}
