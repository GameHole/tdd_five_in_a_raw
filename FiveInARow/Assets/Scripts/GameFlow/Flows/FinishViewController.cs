namespace Five
{
    public class FinishViewController : IGameFinish
    {
        public FinishView finishView;
        public Player player;
        public void Finish(int winChess)
        {
            finishView.IsWin = player.chess == winChess;
            finishView.Open();
        }
    }
}
