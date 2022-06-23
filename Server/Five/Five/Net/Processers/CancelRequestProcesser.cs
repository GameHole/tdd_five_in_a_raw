using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class CancelRequestProcesser : RequestProcesser
    {
        protected override Result processIntarnal(Message message)
        {
            return matcher.Cancel();
        }
    }
}
