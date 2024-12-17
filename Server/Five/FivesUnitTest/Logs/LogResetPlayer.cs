using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    public class LogResetPlayer : Player
    {
        public string log = "";

        public LogResetPlayer(int id):base(id)
        {
        }

        public override void Reset()
        {
            base.Reset();
            log += "Reset ";
        }
    }
}
