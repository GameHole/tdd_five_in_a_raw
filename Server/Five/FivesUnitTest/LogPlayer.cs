using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class LogPlayer : Player
    {
        public string log = "";


        public override void Start(int chess)
        {
            base.Start(chess);
            log += "Start ";
        }
    }
}
