using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class ServerProcesser : MessageProcesser
    {
        public IProcesser connect { get; set; }
        public IProcesser serverStop { get; set; }
        public ServerProcesser(IProcesser defaultProcesser) : base(defaultProcesser)
        {
        }
    }
}
