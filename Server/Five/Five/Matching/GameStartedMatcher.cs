using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class GameStartedMatcher : IMatchable
    {
        public Result Cancel(Matcher player)
        {
            return ResultDefine.GameStarted;
        }

        public Result Match(Matcher player)
        {
            return ResultDefine.GameStarted;
        }
    }
}
