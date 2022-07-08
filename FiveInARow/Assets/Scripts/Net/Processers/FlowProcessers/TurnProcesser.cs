using System;
namespace Five
{
    public class TurnProcesser : AFlowProcesser<PlayerIdNotify>
    {
        public override int OpCode => MessageCode.TurnNotify;

        public override void ProcessContent(PlayerIdNotify message)
        {
            int chess = players.FindChess(message.playerId);
            foreach (var item in flow.GetFlowList<IPlayerTurn>())
            {
                item.Turn(chess);
            }
        }
    }
}
