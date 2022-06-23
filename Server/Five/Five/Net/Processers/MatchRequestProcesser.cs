using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class MatchRequestProcesser : RequestProcesser
    {
        protected override Result processIntarnal(Message message)
        {
            return matcher.Match();
        }
    }
}
