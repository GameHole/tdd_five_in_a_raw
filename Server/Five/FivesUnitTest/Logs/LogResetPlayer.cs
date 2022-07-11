using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    public class LogResetPlayer : Player
    {
        public string log = "";

        public override void Reset()
        {
            base.Reset();
            log += "Reset ";
        }
    }
}
