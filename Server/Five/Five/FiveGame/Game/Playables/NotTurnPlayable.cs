using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class NotTurnPlayable : IPlayable
    {
        public Result Commit(Message message, Player player)
        {
            return ResultDefine.NotCurrentTurnPlayer;
        }
    }
}
