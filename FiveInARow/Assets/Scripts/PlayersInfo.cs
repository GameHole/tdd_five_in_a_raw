using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Five
{
    public class PlayersInfo
    {
        public PlayerInfo[] infos;
        public int FindChess(int id)
        {
            return infos[id].Chess;
        }
    }
}
