using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class CancelRequestProcesser : RequestProcesser
    {
        public override int OpCode => 3;

        protected override Result processIntarnal(Message message)
        {
            return matcher.Cancel();
        }
    }
}
