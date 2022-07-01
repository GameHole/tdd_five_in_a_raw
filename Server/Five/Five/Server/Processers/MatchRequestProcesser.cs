using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class MatchRequestProcesser : RequestProcesser
    {
        public override int OpCode => 1;

        protected override Result processIntarnal(Message message)
        {
            return matcher.Match();
        }
    }
}
