using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    public class LogMatcher : Matcher
    {
        public string log = "";
        public LogMatcher():base(null)
        {
        }
        public LogMatcher(Matching matching) : base(matching)
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
