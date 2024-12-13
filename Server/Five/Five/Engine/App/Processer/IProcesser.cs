using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public interface IProcesser
    {
        void Process(AClient socket,Message message);
    }
}
