using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public interface IProcesser
    {
        int MessageCode { get; }
        void Process(Message message);
    }
}
