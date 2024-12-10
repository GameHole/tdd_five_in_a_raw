using System;
using System.Collections.Generic;
using System.Text;

namespace Five.RTS
{
    public class RTGGameFactroy : IGameFactroy
    {
        public AGame Factroy()
        {
            return new RTSGame();
        }
    }
}
