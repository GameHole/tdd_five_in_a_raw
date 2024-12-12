using Five.RTS;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    internal class LogRTSNotifier : IRTSNotifier
    {
        public string log;

        public void StartRTS(RTSStartNotify notify)
        {
            log += $"Send {notify}";
            
        }
    }
}
