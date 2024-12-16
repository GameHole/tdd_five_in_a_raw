using Five;
using System.Collections.Generic;

namespace FivesUnitTest
{
    internal class TRoom : IRoom
    {
        public Player[] players;
        public IEnumerable<Player> Players => players;

        public int maxPlayer => 2;

        public void Stop()
        {
           
        }
    }
}