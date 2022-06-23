using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class MachingMatcher : IMatchable
    {
        Matching matching;
        public MachingMatcher(Matching matching)
        {
            this.matching = matching;
        }
        public Result Cancel(Matcher player)
        {
            matching.Cancel(player);
            player.Set<DefaultMatcher>();
            return ResultDefine.Success;
        }

        public Result Match(Matcher player)
        {
            return ResultDefine.Matching;
        }
    }
}
