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
        public override Result Play(int x, int y)
        {
            log += $"Play({x},{y}) ";
            return base.Play(x, y);
        }
    }
}
