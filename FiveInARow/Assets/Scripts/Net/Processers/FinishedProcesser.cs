using System;

namespace Five
{
    public class FinishedProcesser : AProcesser<PlayerIdNotify>
    {
        private FinishView finishView;
        private Player player;

        public FinishedProcesser(FinishView finishView, Player player)
        {
            this.finishView = finishView;
            this.player = player;
        }

        public override int OpCode => MessageCode.FinishNotify;

        public override void ProcessContent(PlayerIdNotify message)
        {
            finishView.IsWin = player.id == message.playerId;
            finishView.Open();
        }
    }
}
