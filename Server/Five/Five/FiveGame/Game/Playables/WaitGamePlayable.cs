using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class WaitGamePlayable : IPlayable
    {
        public Result Commit(Message message, Player player)
        {
            return ResultDefine.GameNotStart;
        }
    }
}
