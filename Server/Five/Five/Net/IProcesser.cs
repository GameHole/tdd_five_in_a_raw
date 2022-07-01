using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public interface IProcesser
    {
        int OpCode { get; }
        void Process(Message message);
    }
}
