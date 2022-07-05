using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Five
{
    public class MatchProcesser : AProcesser<MatchResponse>
    {
        private MatchView match;
        private Player player;

        public MatchProcesser(MatchView match, Player player)
        {
            this.match = match;
            this.player = player;
        }

        public override int OpCode => MessageCode.GetResponseCode(MessageCode.RequestMatch);

        public override void ProcessContent(MatchResponse message)
        {
            player.id = message.playerId;
            match.Matched();
        }
    }
}
