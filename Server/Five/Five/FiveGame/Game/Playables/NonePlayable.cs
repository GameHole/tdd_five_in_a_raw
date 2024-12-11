using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class NonePlayable : IPlayable
    {
        public Result Commit(Message message, Player player)
        {
            return ResultDefine.PlayerNotInTheGame;
        }
    }
}
