using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    public class TGame : AGame
    {
        public override Result Commit(Message message, Player player)
        {
            return default;
        }

        public override void Start()
        {
        }

        public override void Stop()
        {
        }
    }
}
