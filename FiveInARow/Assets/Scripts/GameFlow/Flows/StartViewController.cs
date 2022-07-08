namespace Five
{
    public class StartViewController : IGameStart
    {
        public MatchView main;
        public GameView game;
        public void Start(int selfChess)
        {
            main.Close();
            game.Open();
        }
    }
}
