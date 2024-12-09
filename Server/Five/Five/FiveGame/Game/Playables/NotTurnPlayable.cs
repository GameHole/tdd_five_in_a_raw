using System;
using System.Collections.Generic;
using System.Text;

namespace Five
{
    class NotTurnPlayable : IPlayable
    {
        public Result Play(int x, int y, Player player)
        {
            return ResultDefine.NotCurrentTurnPlayer;
        }
    }
}
