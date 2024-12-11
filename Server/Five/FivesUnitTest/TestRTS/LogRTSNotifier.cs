using Five.RTS;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    internal class LogRTSNotifier : IRTSNotifier
    {
        public string log;

        public void StartRTS(List<Charater> info, float fshx, float fshy)
        {
            log += "Start";
            foreach (var item in info)
            {
                log += $"({item.id},{item.x},{item.y})";
            }
            log += $"({fshx},{fshy})";
        }
    }
}
