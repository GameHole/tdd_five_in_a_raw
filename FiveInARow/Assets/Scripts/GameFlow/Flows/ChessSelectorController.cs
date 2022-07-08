namespace Five
{
    public class ChessSelectorController : IGameFinish,IPlayerTurn
    {
        public ChessSelectorView selector;
        public Player player;
        public void Finish(int win)
        {
            selector.Stop();
        }

        public void Turn(int chess)
        {
            if (chess == player.chess)
                selector.Start();
            else
                selector.Stop();
        }
    }
}
