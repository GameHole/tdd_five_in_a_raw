using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class DefaultMatcher : IMatchable
    {
        Matching matching;
        public DefaultMatcher(Matching matching)
        {
            this.matching = matching;
        }
        public Result Cancel(Matcher player)
        {
            return ResultDefine.NotInMatching;
        }

        public Result Match(Matcher player)
        {
            matching.Match(player);
            player.Set<MachingMatcher>();
            return ResultDefine.Success;
        }
    }
}
