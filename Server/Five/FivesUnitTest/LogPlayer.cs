using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class LogPlayer : Player
    {
        public string log = "";
        public override void Start(Game game, int chess)
        {
            base.Start(game, chess);
            log += "Start ";
        }
        public override void Match()
        {
            base.Match();
            log += "Match ";
        }
        public override void CancelMatch()
        {
            base.CancelMatch();
            log += "CancelMatch ";
        }
    }
}
