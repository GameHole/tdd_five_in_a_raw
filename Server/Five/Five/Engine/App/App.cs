using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    public class App : MessageProcesser
    {
        public IProcesser connect { get; set; }
        public IProcesser serverStop { get; set; }
        public App(IProcesser defaultProcesser) : base(defaultProcesser)
        {
        }
    }
}
