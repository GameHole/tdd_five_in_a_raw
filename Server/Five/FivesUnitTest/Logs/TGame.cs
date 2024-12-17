using Five;
using System;
using System.Collections.Generic;
using System.Text;

namespace FivesUnitTest
{
    public class TGame : AGame
    {
        internal object msg;
        internal object player;

        public override Result Commit(Message message, Player player)
        {
            msg = message;
            this.player = player;
            return new Result(-1);
        }

        public override void Start()
        {
        }

        public override void Stop()
        {
        }
    }
}
