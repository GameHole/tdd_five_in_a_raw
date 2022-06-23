using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    class LogMaster : Matcher
    {
        public string log = "";
        public LogMaster():base(null)
        {
        }
        public LogMaster(Matching matching) : base(matching)
        {
        }

        public override void Started()
        {
            base.Started();
            log += "Start ";
        }
        public override void Matched()
        {
            base.Matched();
            log += "Match ";
        }
        public override void Canceled()
        {
            base.Canceled();
            log += "CancelMatch ";
        }
        public override void Finished()
        {
            base.Finished();
            log += "Finished ";
        }

    }
}
