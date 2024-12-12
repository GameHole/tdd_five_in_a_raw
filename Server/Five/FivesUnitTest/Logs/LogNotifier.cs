using Five;
using Five.RTS;
using System.Collections.Generic;

namespace FivesUnitTest
{
    internal class LogNotifier : INotifier
    {
        internal string log;

        public void Send(Message notify)
        {
            log += $"Send {notify} ";
        }
    }
}