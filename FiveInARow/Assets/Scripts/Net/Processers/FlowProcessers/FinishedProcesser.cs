namespace Five
{
    public class FinishedProcesser : AFlowProcesser<PlayerIdNotify>
    {

        public override int OpCode => MessageCode.FinishNotify;

        public override void ProcessContent(PlayerIdNotify message)
        {
            int chess = players.FindChess(message.playerId);
            foreach (var item in flow.GetFlowList<IGameFinish>())
            {
                item.Finish(chess);
            }
        }
    }
}
